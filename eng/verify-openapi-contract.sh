#!/usr/bin/env bash
set -euo pipefail

# Checks the live public BinaryLane OpenAPI document against the committed
# snapshot. It reports drift but never changes source-controlled files.

script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repository_root="$(CDPATH= cd -- "$script_dir/.." && pwd)"
metadata_file="$repository_root/eng/openapi/contract.json"
source_url="${BINARYLANE_OPENAPI_URL:-https://api.binarylane.com.au/reference/openapi.yaml}"
temporary_file="$(mktemp)"

cleanup() {
    rm -f "$temporary_file"
}
trap cleanup EXIT

if [[ ! -f "$metadata_file" ]]; then
    printf 'Committed contract metadata does not exist: %s\n' "$metadata_file" >&2
    exit 2
fi

expected_sha256="$(sed -n 's/^[[:space:]]*"sha256"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\1/p' "$metadata_file")"
expected_version="$(sed -n 's/^[[:space:]]*"upstreamVersion"[[:space:]]*:[[:space:]]*"\([^"]*\)".*/\1/p' "$metadata_file")"

if [[ -z "$expected_sha256" || -z "$expected_version" ]]; then
    printf 'Committed contract metadata is missing a SHA-256 or upstream version.\n' >&2
    exit 2
fi

curl --fail --silent --show-error --location --proto '=https' --proto-redir '=https' \
    --connect-timeout 15 --max-time 90 --retry 3 --retry-all-errors \
    --output "$temporary_file" \
    "$source_url"

if [[ ! -s "$temporary_file" ]]; then
    printf 'Downloaded OpenAPI contract is empty.\n' >&2
    exit 3
fi

actual_sha256="$(sha256sum "$temporary_file" | awk '{print $1}')"
actual_version="$({
    awk '
        /^info:$/ { in_info = 1; next }
        in_info && /^  version: / {
            sub(/^  version: /, "")
            gsub(/[[:space:]]+$/, "")
            print
            exit
        }
    ' "$temporary_file"
} || true)"

if [[ "$expected_sha256" == "$actual_sha256" && "$expected_version" == "$actual_version" ]]; then
    printf 'BinaryLane OpenAPI contract matches committed snapshot (%s, %s).\n' \
        "$actual_version" "$actual_sha256"
    exit 0
fi

printf 'BinaryLane OpenAPI contract changed.\n' >&2
printf '  expected version: %s\n' "$expected_version" >&2
printf '  actual version:   %s\n' "${actual_version:-<unreadable>}" >&2
printf '  expected sha256:  %s\n' "$expected_sha256" >&2
printf '  actual sha256:    %s\n' "$actual_sha256" >&2
printf 'Run ./eng/refresh-openapi-contract.sh, review the diff, then update SDK coverage and tests.\n' >&2
exit 1
