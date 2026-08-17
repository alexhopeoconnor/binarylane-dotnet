using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>
/// Base class for JSON objects returned by the BinaryLane API.
/// </summary>
/// <remarks>
/// BinaryLane's v2 API is a developer preview and may add properties before this
/// SDK is updated. Unknown properties are retained instead of causing a
/// deserialization failure, so applications that need an early provider field can
/// opt in to reading it from <see cref="AdditionalProperties"/>.
/// </remarks>
public abstract class BinaryLaneDto
{
    /// <summary>
    /// Gets properties supplied by the service that this version of the SDK does
    /// not yet model explicitly.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? AdditionalProperties { get; init; }
}

/// <summary>
/// Base class for request payloads accepted by the BinaryLane API.
/// </summary>
public abstract class BinaryLaneRequestModel : BinaryLaneDto
{
}
