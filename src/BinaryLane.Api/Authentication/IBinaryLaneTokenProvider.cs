using System.Threading;
using System.Threading.Tasks;

namespace BinaryLane.Api.V2.Authentication;

/// <summary>Supplies a BinaryLane bearer token for an outgoing request.</summary>
public interface IBinaryLaneTokenProvider
{
    /// <summary>Gets the current bearer token.</summary>
    ValueTask<string> GetTokenAsync(CancellationToken cancellationToken = default);
}
