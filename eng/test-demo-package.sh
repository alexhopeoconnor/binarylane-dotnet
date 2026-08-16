#!/usr/bin/env bash
set -euo pipefail

# Compiles the tracked demo against the nupkg just produced by this repository,
# rather than against the local source project. This catches bad package
# metadata, missing dependencies, and public API regressions.

if [[ $# -ne 1 ]]; then
    printf 'Usage: %s <package-directory>\n' "${0##*/}" >&2
    exit 2
fi

package_directory="$(CDPATH= cd -- "$1" && pwd)"
script_dir="$(CDPATH= cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
repository_root="$(CDPATH= cd -- "$script_dir/.." && pwd)"
demo_source="$repository_root/examples/BinaryLane.Api.Demo"
temporary_directory="$(mktemp -d)"
export NUGET_PACKAGES="$temporary_directory/nuget-packages"

cleanup() {
    rm -rf "$temporary_directory"
}
trap cleanup EXIT

package_file="$(find "$package_directory" -maxdepth 1 -type f -name 'BinaryLane.Api.*.nupkg' -print -quit)"
if [[ -z "$package_file" ]]; then
    printf 'No BinaryLane.Api .nupkg found in %s.\n' "$package_directory" >&2
    exit 2
fi

package_name="$(basename -- "$package_file")"
package_version="${package_name#BinaryLane.Api.}"
package_version="${package_version%.nupkg}"
demo_directory="$temporary_directory/BinaryLane.Api.Demo"

cp -R "$demo_source" "$demo_directory"
cp "$repository_root/Directory.Build.props" "$temporary_directory/Directory.Build.props"
cp "$repository_root/Directory.Packages.props" "$temporary_directory/Directory.Packages.props"
demo_project="$demo_directory/BinaryLane.Api.Demo.csproj"

if ! grep -Fq '<ProjectReference Include="../../src/BinaryLane.Api/BinaryLane.Api.csproj" />' "$demo_project"; then
    printf 'Demo project does not contain the expected local SDK project reference.\n' >&2
    exit 2
fi

sed -i "/<\\/ItemGroup>/i\\    <PackageVersion Include=\"BinaryLane.Api\" Version=\"$package_version\" />" \
    "$temporary_directory/Directory.Packages.props"

sed -i "s#<ProjectReference Include=\"../../src/BinaryLane.Api/BinaryLane.Api.csproj\" />#<PackageReference Include=\"BinaryLane.Api\" />#" \
    "$demo_project"

dotnet restore "$demo_project" \
    --source "$package_directory" \
    --source https://api.nuget.org/v3/index.json \
    --no-cache \
    -p:RestoreLockedMode=false
dotnet build "$demo_project" \
    --configuration Release \
    --no-restore \
    -m:1 \
    -nodeReuse:false \
    -p:BuildInParallel=false

printf 'Demo compiled against BinaryLane.Api %s from %s.\n' \
    "$package_version" "$package_directory"
