using System;

namespace BinaryLane.Api.V2.Configuration;

/// <summary>Configures a BinaryLane API client.</summary>
public sealed class BinaryLaneOptions
{
    /// <summary>The configuration section convention used by consuming applications.</summary>
    public const string SectionName = "BinaryLane";

    /// <summary>The BinaryLane API root URL.</summary>
    public string BaseUrl { get; set; } = "https://api.binarylane.com.au/";

    /// <summary>
    /// The bearer token to use when the default token provider is registered. Prefer a secret store,
    /// user secrets, or environment-variable based configuration rather than committing this value.
    /// </summary>
    public string? ApiToken { get; set; }

    /// <summary>Timeout applied to individual HTTP requests.</summary>
    public int RequestTimeoutSeconds { get; set; } = 100;

    internal static bool IsValid(BinaryLaneOptions options)
    {
        if (options.RequestTimeoutSeconds < 1 || options.RequestTimeoutSeconds > 300)
        {
            return false;
        }

        return Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out var uri)
            && uri.Scheme == Uri.UriSchemeHttps
            && string.IsNullOrEmpty(uri.UserInfo)
            && string.IsNullOrEmpty(uri.Query)
            && string.IsNullOrEmpty(uri.Fragment);
    }
}
