using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>Information about the authenticated BinaryLane account.</summary>
public sealed class Account : BinaryLaneDto
{
    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("email_verified")]
    public bool EmailVerified { get; init; }

    [JsonPropertyName("two_factor_authentication_enabled")]
    public bool TwoFactorAuthenticationEnabled { get; init; }

    /// <summary>Provider status, for example <c>active</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("tax_code")]
    public TaxCode TaxCode { get; init; } = new();

    /// <summary>Provider payment-method values, for example <c>credit-card</c>.</summary>
    [JsonPropertyName("configured_payment_methods")]
    public IReadOnlyList<string> ConfiguredPaymentMethods { get; init; } = Array.Empty<string>();

    [JsonPropertyName("additional_ipv4_limit")]
    public int AdditionalIpv4Limit { get; init; }
}

/// <summary>A tax code currently applicable to an account or invoice.</summary>
public sealed class TaxCode : BinaryLaneDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Provider tax-code type, for example <c>none</c> or <c>scalar</c>.</summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("fixed_percent")]
    public double? FixedPercent { get; init; }
}

public sealed class AccountResponse : BinaryLaneDto
{
    [JsonPropertyName("account")]
    public Account Account { get; init; } = new();
}
