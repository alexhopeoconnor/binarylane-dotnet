using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>The current billing balance for the authenticated account.</summary>
public sealed class Balance : BinaryLaneDto
{
    [JsonPropertyName("unbilled_total")]
    public double UnbilledTotal { get; init; }

    [JsonPropertyName("available_credit")]
    public double AvailableCredit { get; init; }

    [JsonPropertyName("charges")]
    public IReadOnlyList<ChargeInformation> Charges { get; init; } = Array.Empty<ChargeInformation>();

    [JsonPropertyName("generated_at")]
    public DateTimeOffset? GeneratedAt { get; init; }
}

/// <summary>A charge included in a balance response.</summary>
public sealed class ChargeInformation : BinaryLaneDto
{
    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("total")]
    public double Total { get; init; }

    [JsonPropertyName("ongoing")]
    public bool Ongoing { get; init; }
}

/// <summary>A BinaryLane invoice.</summary>
public sealed class Invoice : BinaryLaneDto
{
    [JsonPropertyName("invoice_id")]
    public long InvoiceId { get; init; }

    [JsonPropertyName("reference")]
    public string? Reference { get; init; }

    [JsonPropertyName("invoice_number")]
    public string InvoiceNumber { get; init; } = string.Empty;

    [JsonPropertyName("amount")]
    public double Amount { get; init; }

    [JsonPropertyName("tax_code")]
    public TaxCode TaxCode { get; init; } = new();

    [JsonPropertyName("tax")]
    public double Tax { get; init; }

    [JsonPropertyName("created")]
    public DateTimeOffset Created { get; init; }

    [JsonPropertyName("date_due")]
    public DateTimeOffset DateDue { get; init; }

    [JsonPropertyName("date_overdue")]
    public DateTimeOffset DateOverdue { get; init; }

    [JsonPropertyName("paid")]
    public bool Paid { get; init; }

    [JsonPropertyName("refunded")]
    public bool Refunded { get; init; }

    [JsonPropertyName("payment_failure_count")]
    public int? PaymentFailureCount { get; init; }

    [JsonPropertyName("invoice_items")]
    public IReadOnlyList<InvoiceLineItem> InvoiceItems { get; init; } = Array.Empty<InvoiceLineItem>();

    [JsonPropertyName("invoice_download_url")]
    public string? InvoiceDownloadUrl { get; init; }

    [JsonPropertyName("invoice_view_url")]
    public string? InvoiceViewUrl { get; init; }
}

/// <summary>A line item in a BinaryLane invoice.</summary>
public sealed class InvoiceLineItem : BinaryLaneDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("amount")]
    public double Amount { get; init; }

    [JsonPropertyName("amount_includes_tax")]
    public bool AmountIncludesTax { get; init; }
}

public sealed class BalanceResponse : BinaryLaneDto
{
    [JsonPropertyName("balance")]
    public Balance Balance { get; init; } = new();
}

public sealed class InvoiceResponse : BinaryLaneDto
{
    [JsonPropertyName("invoice")]
    public Invoice Invoice { get; init; } = new();
}

public sealed class InvoicesResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("invoices")]
    public IReadOnlyList<Invoice> Invoices { get; init; } = Array.Empty<Invoice>();
}

public sealed class UnpaidFailedInvoicesResponse : BinaryLaneDto
{
    [JsonPropertyName("invoices")]
    public IReadOnlyList<Invoice> Invoices { get; init; } = Array.Empty<Invoice>();
}
