using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>
/// Base payload for <c>POST /v2/servers/{server_id}/actions</c>.
/// </summary>
/// <remarks>
/// The upstream contract is a discriminated union on <c>type</c>. Its public
/// documentation also creates fragment-bearing pseudo-paths for each variant;
/// those are not separate HTTP routes. Prefer a concrete derived type for a
/// documented action. <see cref="UnknownServerAction"/> permits new provider
/// action types without waiting for an SDK release.
/// </remarks>
public class ServerAction : BinaryLaneRequestModel
{
    /// <summary>Creates an empty action for serializers and advanced callers.</summary>
    public ServerAction()
    {
    }

    /// <summary>Creates a typed action with the provider discriminator value.</summary>
    protected ServerAction(string type)
    {
        Type = type;
    }

    [JsonPropertyName(ServerActionJsonNames.Type)]
    public string Type { get; init; } = string.Empty;
}

internal static class ServerActionJsonNames
{
    internal const string Type = "type";
}

/// <summary>An action type not yet represented by a concrete SDK class.</summary>
public sealed class UnknownServerAction : ServerAction
{
    public UnknownServerAction(string type)
        : base(type)
    {
    }
}
