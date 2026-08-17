using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Models;
using BinaryLane.Api.V2.Pagination;

namespace BinaryLane.Api.V2.Resources;

/// <summary>Reads information about the authenticated account.</summary>
public interface IAccountApi
{
    /// <summary>Gets the current account.</summary>
    Task<Account> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the untyped account response for forward compatibility.</summary>
    Task<JsonElement> GetRawAsync(CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class AccountApi : BinaryLaneResourceBase, IAccountApi
{
    /// <summary>Initializes the account resource.</summary>
    public AccountApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Account> GetAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<Account>("v2/account", "account", cancellationToken);

    /// <inheritdoc />
    public Task<JsonElement> GetRawAsync(CancellationToken cancellationToken = default) =>
        base.GetRawAsync("v2/account", null, cancellationToken);
}
