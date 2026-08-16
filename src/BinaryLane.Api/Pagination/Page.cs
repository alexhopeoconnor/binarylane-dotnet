using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BinaryLane.Api.V2.Pagination;

/// <summary>A page returned from a BinaryLane list endpoint.</summary>
/// <typeparam name="T">Item type.</typeparam>
public sealed class Page<T>
{
    internal Page(
        IReadOnlyList<T> items,
        int total,
        Uri? firstPage,
        Uri? previousPage,
        Uri? nextPage,
        Uri? lastPage)
    {
        Items = new ReadOnlyCollection<T>(new List<T>(items));
        Total = total;
        FirstPage = firstPage;
        PreviousPage = previousPage;
        NextPage = nextPage;
        LastPage = lastPage;
    }

    /// <summary>Items returned in this response.</summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>Total number of items available across all pages, when supplied by BinaryLane.</summary>
    public int Total { get; }

    /// <summary>First result page, when supplied by BinaryLane.</summary>
    public Uri? FirstPage { get; }

    /// <summary>Previous result page, when supplied by BinaryLane.</summary>
    public Uri? PreviousPage { get; }

    /// <summary>Next result page, when supplied by BinaryLane.</summary>
    public Uri? NextPage { get; }

    /// <summary>Last result page, when supplied by BinaryLane.</summary>
    public Uri? LastPage { get; }
}
