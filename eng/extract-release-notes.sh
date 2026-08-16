#!/usr/bin/env bash
set -euo pipefail

if [[ $# -ne 1 ]]; then
    printf 'Usage: %s <package-version>\n' "${0##*/}" >&2
    exit 2
fi

package_version="$1"
script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repository_root="$(CDPATH= cd -- "$script_dir/.." && pwd)"
changelog_file="$repository_root/CHANGELOG.md"

notes="$(awk -v bracket_heading="## [$package_version]" -v bare_heading="## $package_version" '
    $0 == bracket_heading || index($0, bracket_heading " -") == 1 ||
    $0 == bare_heading || index($0, bare_heading " -") == 1 {
        capture = 1
        next
    }

    capture && /^## / {
        exit
    }

    capture && /^\[/ {
        exit
    }

    capture {
        print
    }
' "$changelog_file")"

if [[ -z "${notes//[[:space:]]/}" ]]; then
    printf 'No release notes found for %s in CHANGELOG.md.\n' "$package_version" >&2
    exit 1
fi

printf '%s\n' "$notes"
