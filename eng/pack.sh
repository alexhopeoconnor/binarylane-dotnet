#!/usr/bin/env bash
set -euo pipefail

script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repository_root="$(CDPATH= cd -- "$script_dir/.." && pwd)"
output_directory="${1:-$repository_root/artifacts}"

dotnet pack "$repository_root/src/BinaryLane.Api/BinaryLane.Api.csproj" \
    --configuration Release \
    --no-restore \
    -m:1 \
    -nodeReuse:false \
    -p:BuildInParallel=false \
    --output "$output_directory"
