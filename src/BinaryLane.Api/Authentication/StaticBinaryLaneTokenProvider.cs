using System;
using System.Threading;
using System.Threading.Tasks;

namespace BinaryLane.Api.V2.Authentication;

/// <summary>Provides a fixed token. Prefer a custom provider when tokens rotate.</summary>
public sealed class StaticBinaryLaneTokenProvider : IBinaryLaneTokenProvider
{
    private readonly string _token;

    /// <summary>Initializes the provider with a non-empty bearer token.</summary>
    public StaticBinaryLaneTokenProvider(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("A BinaryLane API token is required.", nameof(token));
        }

        _token = token;
    }

    /// <inheritdoc />
    public ValueTask<string> GetTokenAsync(CancellationToken cancellationToken = default) =>
        new(_token);
}
