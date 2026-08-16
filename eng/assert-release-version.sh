#!/usr/bin/env bash
set -euo pipefail

# Enforces the release identity before any package is published. The version is
# intentionally read from the package project so one source of truth controls
# both the .nupkg and its corresponding Git tag.

if [[ $# -ne 1 ]]; then
    printf 'Usage: %s v<package-version>\n' "${0##*/}" >&2
    exit 2
fi

tag_name="$1"
script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repository_root="$(CDPATH= cd -- "$script_dir/.." && pwd)"
changelog_file="$repository_root/CHANGELOG.md"

if [[ ! -f "$changelog_file" ]]; then
    printf 'Expected changelog does not exist.\n' >&2
    exit 2
fi

package_version="$("$script_dir/read-package-version.sh")"

expected_tag="v${package_version}"
if [[ "$tag_name" != "$expected_tag" ]]; then
    printf 'Tag %s does not match package version %s (expected %s).\n' \
        "$tag_name" "$package_version" "$expected_tag" >&2
    exit 1
fi

if ! grep -Fqx "## [$package_version]" "$changelog_file" && \
   ! grep -Fq "## [$package_version] -" "$changelog_file" && \
   ! grep -Fqx "## $package_version" "$changelog_file" && \
   ! grep -Fq "## $package_version -" "$changelog_file"; then
    printf 'CHANGELOG.md needs a level-two heading for %s.\n' "$package_version" >&2
    exit 1
fi

printf 'Release version is consistent: %s.\n' "$package_version"
