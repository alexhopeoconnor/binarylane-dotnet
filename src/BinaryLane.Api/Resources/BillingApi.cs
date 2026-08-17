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

/// <summary>Reads account billing and invoice information.</summary>
public interface IBillingApi
{
    Task<Balance> GetBalanceAsync(CancellationToken cancellationToken = default);
    Task<Invoice> GetInvoiceAsync(long invoiceId, CancellationToken cancellationToken = default);
    Task<Page<Invoice>> ListInvoicesAsync(PageRequest? page = null, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Invoice>> ListUnpaidPaymentFailedInvoicesAsync(CancellationToken cancellationToken = default);
}

/// <inheritdoc />
public sealed class BillingApi : BinaryLaneResourceBase, IBillingApi
{
    /// <summary>Initializes the billing resource.</summary>
    public BillingApi(IBinaryLaneApiExecutor executor, BinaryLaneJsonSerializerOptions json)
        : base(executor, json)
    {
    }

    /// <inheritdoc />
    public Task<Balance> GetBalanceAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<Balance>("v2/customers/my/balance", "balance", cancellationToken);

    /// <inheritdoc />
    public Task<Invoice> GetInvoiceAsync(long invoiceId, CancellationToken cancellationToken = default) =>
        GetItemAsync<Invoice>($"v2/customers/my/invoices/{invoiceId}", "invoice", cancellationToken);

    /// <inheritdoc />
    public Task<Page<Invoice>> ListInvoicesAsync(PageRequest? page = null, CancellationToken cancellationToken = default) =>
        GetPageAsync<Invoice>("v2/customers/my/invoices", "invoices", page, null, cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyList<Invoice>> ListUnpaidPaymentFailedInvoicesAsync(CancellationToken cancellationToken = default) =>
        GetItemAsync<IReadOnlyList<Invoice>>("v2/customers/my/unpaid-payment-failed-invoices", "invoices", cancellationToken);
}
