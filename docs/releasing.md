# Maintainer guide: releases

This page is for package maintainers.

This repository publishes packages with NuGet trusted publishing and GitHub
OpenID Connect (OIDC). It must not use a long-lived NuGet API key.

## One-time setup

1. On NuGet.org, create or verify the trusted-publishing policy:
   - publisher: `GitHubActions`;
   - GitHub owner: `alexhopeoconnor`;
   - repository: `binarylane-dotnet`;
   - workflow: `publish.yml`;
   - environment: `release`.
2. In GitHub, create a protected environment named `release` and require a
   maintainer approval.
3. Create a protected `release` environment secret named `NUGET_USER` with
   the NuGet.org username `alex.hope.oconnor`. It is an identifier, not an
   API key.
4. Protect `main` with CI and restrict creation of `v*` tags.

The first successful OIDC publication must occur before NuGet's policy
activation window expires. After success, NuGet permanently activates the
policy for this exact repository/workflow/environment identity.

## Release checklist

1. Review BinaryLane contract changes and update the committed snapshot if
   necessary.
2. Update code, tests, docs, API coverage, and `CHANGELOG.md`.
3. Set the package `<Version>` to the intended SemVer release.
4. Open and merge the release pull request after CI succeeds.
5. Tag the exact merge commit. The tag must equal the package version with a
   leading `v`.

```bash
git tag -a v<package-version> -m "BinaryLane.Api <package-version>"
git push origin v<package-version>
```

6. Approve the protected `release` environment in GitHub Actions.
7. Verify the package page, rendered README, icon, license, repository link,
   symbols, package ownership, and GitHub Release.

## What the release workflow does

`publish.yml` validates the tag, package version, and changelog heading; it
then restores, builds, tests, packs, and compiles the maintained demo against
the resulting local `.nupkg`. Only then does `NuGet/login@v1` exchange the
GitHub OIDC identity for a short-lived one-time publish key. The key is used
only in memory by the job.

The GitHub Release job runs after NuGet publishing and receives no OIDC
permission. It attaches the package files and uses the matching changelog
section for release notes.

## Recovery

NuGet packages cannot be overwritten. If a published release has a packaging
defect, unlist it if appropriate, publish a new version, and document the
correction in the changelog. A tag can be replaced only before the workflow
publishes its matching NuGet version; published package versions are never
reused.
