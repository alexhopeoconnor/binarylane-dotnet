using System;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Configuration;
using Microsoft.Extensions.Options;

namespace BinaryLane.Api.V2.Authentication;

/// <summary>Uses the token configured in <see cref="BinaryLaneOptions"/>.</summary>
public sealed class OptionsBinaryLaneTokenProvider : IBinaryLaneTokenProvider
{
    private readonly IOptionsMonitor<BinaryLaneOptions> _options;

    /// <summary>Initializes the provider.</summary>
    public OptionsBinaryLaneTokenProvider(IOptionsMonitor<BinaryLaneOptions> options) =>
        _options = options ?? throw new ArgumentNullException(nameof(options));

    /// <inheritdoc />
    public ValueTask<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        var token = _options.CurrentValue.ApiToken;
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException(
                "No BinaryLane API token is configured. Set BinaryLaneOptions.ApiToken or register a custom IBinaryLaneTokenProvider.");
        }

        return new ValueTask<string>(token!);
    }
}
