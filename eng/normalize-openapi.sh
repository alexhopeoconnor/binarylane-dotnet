#!/usr/bin/env bash
set -euo pipefail

# Normalize BinaryLane's raw OpenAPI document for code-generation or coverage
# tooling. The reference includes virtual `#ActionName` paths for individual
# server action payloads. They are documentation aliases, not HTTP endpoints.

script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
default_input="$script_dir/openapi/binarylane-v2.openapi.yaml"
input_file="${1:-$default_input}"
output_file="${2:-}"

if [[ ! -f "$input_file" ]]; then
    printf 'OpenAPI input does not exist: %s\n' "$input_file" >&2
    exit 2
fi

normalize() {
    awk '
        /^paths:$/ {
            in_paths = 1
        }

        in_paths && /^components:$/ {
            in_paths = 0
            skip_virtual_path = 0
        }

        in_paths && /^  ['"'"'][^'"'"']*#[^'"'"']*['"'"']:[[:space:]]*$/ {
            skip_virtual_path = 1
            removed += 1
            next
        }

        in_paths && /^  [^[:space:]][^:]*:[[:space:]]*$/ {
            skip_virtual_path = 0
        }

        !skip_virtual_path {
            print
        }

        END {
            printf "Removed %d virtual action path(s).\n", removed > "/dev/stderr"
        }
    ' "$input_file"
}

if [[ -n "$output_file" ]]; then
    mkdir -p "$(dirname -- "$output_file")"
    normalize > "$output_file"
else
    normalize
fi
