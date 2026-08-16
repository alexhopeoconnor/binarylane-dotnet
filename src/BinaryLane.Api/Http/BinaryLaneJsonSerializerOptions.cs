using System.Text.Json;
using System.Text.Json.Serialization;
using BinaryLane.Api.V2.Models;

namespace BinaryLane.Api.V2.Http;

/// <summary>JSON settings used by the BinaryLane API client.</summary>
public sealed class BinaryLaneJsonSerializerOptions
{
    /// <summary>Creates the default BinaryLane JSON settings.</summary>
    public BinaryLaneJsonSerializerOptions()
    {
        SerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        SerializerOptions.Converters.Add(new ServerActionJsonConverter());
    }

    /// <summary>The serializer settings used for request and response bodies.</summary>
    public JsonSerializerOptions SerializerOptions { get; }
}
