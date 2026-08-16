# Changelog

All notable changes to this project are documented here. This project follows
[Semantic Versioning](https://semver.org/). The SDK version describes the
public .NET API; it is deliberately independent from BinaryLane's preview API
version.

## [Unreleased]

### Added

- Nothing yet.

## [1.0.0] - 2026-08-17

### Added

- First stable release of the BinaryLane v2 .NET client, following the public
  `0.1.0-beta.1` validation release.
- Package validation for the `net8.0` and `netstandard2.0` public API assets.

## [0.1.0-beta.1] - 2026-08-16

### Added

- Initial preview release of the unofficial community .NET client for the
  BinaryLane v2 API.
- Typed HTTP client setup, pluggable bearer-token authentication, pagination,
  API error mapping, and a maintained read-only demo application.
- HTTPS-only bearer-token transport, protected request authority and
  authorization headers, and a 16 MiB successful-response buffer limit.
- OpenAPI contract snapshot and change-monitoring automation for BinaryLane
  API reference version `0.39.1`.

[Unreleased]: https://github.com/alexhopeoconnor/binarylane-dotnet/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/alexhopeoconnor/binarylane-dotnet/releases/tag/v1.0.0
[0.1.0-beta.1]: https://github.com/alexhopeoconnor/binarylane-dotnet/releases/tag/v0.1.0-beta.1
