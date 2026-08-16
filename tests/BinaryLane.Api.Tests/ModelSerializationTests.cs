using System.Text.Json;
using BinaryLane.Api.V2.Http;
using BinaryLane.Api.V2.Models;
using Xunit;

namespace BinaryLane.Api.Tests;

public sealed class ModelSerializationTests
{
    private static readonly JsonSerializerOptions Json =
        new BinaryLaneJsonSerializerOptions().SerializerOptions;

    [Fact]
    public void PowerOnActionSerializesTheProviderDiscriminator()
    {
        string json = JsonSerializer.Serialize<ServerAction>(new PowerOnServerAction(), Json);

        using JsonDocument document = JsonDocument.Parse(json);
        Assert.Equal("power_on", document.RootElement.GetProperty("type").GetString());
        Assert.Single(document.RootElement.EnumerateObject());
    }

    [Fact]
    public void CreateServerRequestSerializesBinaryLaneWireNamesAndOmitsNulls()
    {
        CreateServerRequest request = new()
        {
            Name = "demo-server",
            Image = "ubuntu-24.04",
            Region = "melbourne",
            Size = "std-min",
            PortBlocking = false,
        };

        string json = JsonSerializer.Serialize(request, Json);
        using JsonDocument document = JsonDocument.Parse(json);

        Assert.Equal("demo-server", document.RootElement.GetProperty("name").GetString());
        Assert.Equal("ubuntu-24.04", document.RootElement.GetProperty("image").GetString());
        Assert.Equal("melbourne", document.RootElement.GetProperty("region").GetString());
        Assert.Equal("std-min", document.RootElement.GetProperty("size").GetString());
        Assert.False(document.RootElement.GetProperty("port_blocking").GetBoolean());
        Assert.False(document.RootElement.TryGetProperty("password", out _));
        Assert.False(document.RootElement.TryGetProperty("user_data", out _));
    }

    [Fact]
    public void UnknownResponsePropertiesAreRetainedForForwardCompatibility()
    {
        const string json = """
            {
              "email": "alex@example.test",
              "email_verified": true,
              "two_factor_authentication_enabled": false,
              "status": "active",
              "tax_code": { "name": "GST", "type": "scalar" },
              "configured_payment_methods": ["credit-card"],
              "additional_ipv4_limit": 2,
              "future_provider_field": "kept"
            }
            """;

        Account? account = JsonSerializer.Deserialize<Account>(json, Json);

        Assert.NotNull(account);
        Assert.Equal("active", account.Status);
        Assert.NotNull(account.AdditionalProperties);
        Assert.Equal(
            "kept",
            account.AdditionalProperties!["future_provider_field"].GetString());
    }

    [Fact]
    public void PreviewEnumValuesRemainRawStrings()
    {
        const string json = """
            {
              "id": 42,
              "status": "future-status",
              "type": "future-action",
              "started_at": "2026-08-16T12:34:56Z",
              "title": "Future action",
              "reason": "A future provider action",
              "progress": { "percent_complete": 0, "completed_steps": [] }
            }
            """;

        BinaryLaneAction? action = JsonSerializer.Deserialize<BinaryLaneAction>(json, Json);

        Assert.NotNull(action);
        Assert.Equal("future-status", action.Status);
        Assert.Equal("future-action", action.Type);
    }

    [Fact]
    public void DocumentedValuesAreAvailableWithoutClosedEnums()
    {
        Assert.Equal("completed", BinaryLaneValues.ActionStatus.Completed);
        Assert.Equal("power_on", BinaryLaneValues.ServerActionType.PowerOn);
        Assert.Equal("AAAA", BinaryLaneValues.DomainRecordType.Aaaa);
    }

    [Fact]
    public void ComplexServerActionUsesTheSharedEndpointDiscriminatorAndWireNames()
    {
        ServerAction action = new ChangeVpcIpv4ServerAction
        {
            CurrentIpv4Address = "10.0.0.10",
            NewIpv4Address = "10.0.0.20",
        };

        string json = JsonSerializer.Serialize(action, Json);
        using JsonDocument document = JsonDocument.Parse(json);

        Assert.Equal("change_vpc_ipv4", document.RootElement.GetProperty("type").GetString());
        Assert.Equal("10.0.0.10", document.RootElement.GetProperty("current_ipv4_address").GetString());
        Assert.Equal("10.0.0.20", document.RootElement.GetProperty("new_ipv4_address").GetString());
    }

    [Fact]
    public void ServerActionConverterDeserializesKnownAndUnknownProviderActions()
    {
        const string knownJson = """
            {
              "type": "change_vpc_ipv4",
              "current_ipv4_address": "10.0.0.10",
              "new_ipv4_address": "10.0.0.20"
            }
            """;
        const string unknownJson = """
            {
              "type": "future_action",
              "provider_option": "kept"
            }
            """;

        ServerAction? known = JsonSerializer.Deserialize<ServerAction>(knownJson, Json);
        ServerAction? unknown = JsonSerializer.Deserialize<ServerAction>(unknownJson, Json);

        ChangeVpcIpv4ServerAction typed = Assert.IsType<ChangeVpcIpv4ServerAction>(known);
        Assert.Equal("10.0.0.10", typed.CurrentIpv4Address);
        Assert.Equal("10.0.0.20", typed.NewIpv4Address);

        UnknownServerAction future = Assert.IsType<UnknownServerAction>(unknown);
        Assert.Equal("future_action", future.Type);
        Assert.NotNull(future.AdditionalProperties);
        Assert.Equal("kept", future.AdditionalProperties!["provider_option"].GetString());
    }
}
