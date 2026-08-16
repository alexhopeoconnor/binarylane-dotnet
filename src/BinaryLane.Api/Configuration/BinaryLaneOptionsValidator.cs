using Microsoft.Extensions.Options;

namespace BinaryLane.Api.V2.Configuration;

/// <summary>Validates client options at application startup.</summary>
public sealed class BinaryLaneOptionsValidator : IValidateOptions<BinaryLaneOptions>
{
    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, BinaryLaneOptions options)
    {
        if (options is null)
        {
            return ValidateOptionsResult.Fail("BinaryLane options are required.");
        }

        if (!BinaryLaneOptions.IsValid(options))
        {
            return ValidateOptionsResult.Fail(
                "BinaryLane BaseUrl must be an absolute HTTPS URL and RequestTimeoutSeconds must be between 1 and 300.");
        }

        return ValidateOptionsResult.Success;
    }
}
