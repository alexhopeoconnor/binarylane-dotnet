using System;
using System.Collections.Generic;

namespace BinaryLane.Api.V2.Pagination;

/// <summary>Specifies a one-based BinaryLane API result page.</summary>
public sealed class PageRequest
{
    /// <summary>One-based page number.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Number of results to return, from 1 through 200.</summary>
    public int PerPage { get; set; } = 20;

    internal IReadOnlyDictionary<string, object?> ToQuery()
    {
        if (Page < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(Page), "Page numbering starts at 1.");
        }

        if (PerPage < 1 || PerPage > 200)
        {
            throw new ArgumentOutOfRangeException(nameof(PerPage), "PerPage must be between 1 and 200.");
        }

        return new Dictionary<string, object?>
        {
            ["page"] = Page,
            ["per_page"] = PerPage,
        };
    }
}
