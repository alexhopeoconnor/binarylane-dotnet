#!/usr/bin/env bash
set -euo pipefail

# Downloads the public upstream contract into the reviewed repository snapshot.
# Run this only as part of a human-reviewed change; CI intentionally verifies
# and reports drift instead of changing the snapshot by itself.

script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repository_root="$(CDPATH= cd -- "$script_dir/.." && pwd)"
contract_directory="$repository_root/eng/openapi"
contract_file="$contract_directory/binarylane-v2.openapi.yaml"
metadata_file="$contract_directory/contract.json"
source_url="${BINARYLANE_OPENAPI_URL:-https://api.binarylane.com.au/reference/openapi.yaml}"
temporary_file="$(mktemp)"

cleanup() {
    rm -f "$temporary_file"
}
trap cleanup EXIT

mkdir -p "$contract_directory"
curl --fail --silent --show-error --location --proto '=https' --proto-redir '=https' \
    --connect-timeout 15 --max-time 90 --retry 3 --retry-all-errors \
    --output "$temporary_file" \
    "$source_url"

if [[ ! -s "$temporary_file" ]]; then
    printf 'Downloaded OpenAPI contract is empty.\n' >&2
    exit 1
fi

upstream_version="$({
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

if [[ -z "$upstream_version" ]]; then
    printf 'Unable to read info.version from the downloaded OpenAPI contract.\n' >&2
    exit 1
fi

sha256="$(sha256sum "$temporary_file" | awk '{print $1}')"
retrieved_utc="$(date -u +%Y-%m-%dT%H:%M:%SZ)"

cp "$temporary_file" "$contract_file"
printf '{\n  "source": "%s",\n  "upstreamVersion": "%s",\n  "sha256": "%s",\n  "retrievedUtc": "%s"\n}\n' \
    "$source_url" "$upstream_version" "$sha256" "$retrieved_utc" > "$metadata_file"

printf 'Updated %s (upstream %s, sha256 %s).\n' \
    "$contract_file" "$upstream_version" "$sha256"
