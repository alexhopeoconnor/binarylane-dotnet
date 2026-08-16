#!/usr/bin/env bash
set -euo pipefail

script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
project_file="$(CDPATH= cd -- "$script_dir/.." && pwd)/src/BinaryLane.Api/BinaryLane.Api.csproj"

if [[ ! -f "$project_file" ]]; then
    printf 'Package project does not exist: %s\n' "$project_file" >&2
    exit 2
fi

read_project_property() {
    local property_name="$1"
    sed -n "s@^[[:space:]]*<${property_name}>[[:space:]]*\\([^<]*\\)[[:space:]]*</${property_name}>.*@\\1@p" \
        "$project_file" | head -n 1
}

package_version="$(read_project_property PackageVersion)"
if [[ -z "$package_version" ]]; then
    package_version="$(read_project_property Version)"
fi

if [[ -z "$package_version" ]]; then
    printf 'Package project must contain a literal <PackageVersion> or <Version>.\n' >&2
    exit 2
fi

printf '%s\n' "$package_version"
