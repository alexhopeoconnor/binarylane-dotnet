using System.Net.Http;

namespace BinaryLane.Api.V2.Http;

/// <summary>HTTP methods required by the BinaryLane API beyond the framework's static set.</summary>
internal static class BinaryLaneHttpMethods
{
    internal static readonly HttpMethod Patch = new("PATCH");
}
