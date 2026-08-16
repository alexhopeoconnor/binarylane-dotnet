using System;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Controls opt-in polling of a BinaryLane asynchronous action.</summary>
public sealed class ActionWaitOptions
{
    /// <summary>Delay between action status checks.</summary>
    public TimeSpan PollInterval { get; set; } = TimeSpan.FromSeconds(2);

    /// <summary>Maximum time spent polling before a timeout is reported.</summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(15);

    internal void Validate()
    {
        if (PollInterval < TimeSpan.FromMilliseconds(100) || PollInterval > TimeSpan.FromMinutes(5))
        {
            throw new ArgumentOutOfRangeException(nameof(PollInterval), "PollInterval must be between 100 milliseconds and five minutes.");
        }

        if (Timeout < PollInterval || Timeout > TimeSpan.FromHours(24))
        {
            throw new ArgumentOutOfRangeException(nameof(Timeout), "Timeout must be at least PollInterval and no more than 24 hours.");
        }
    }
}
