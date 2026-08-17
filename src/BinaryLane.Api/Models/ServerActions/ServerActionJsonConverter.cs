using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>
/// Serializes the BinaryLane server-action discriminated union when callers hold
/// an action through its <see cref="ServerAction"/> base type.
/// </summary>
/// <remarks>
/// System.Text.Json normally uses the statically declared type. That would emit
/// only <c>type</c> for a <see cref="ServerAction"/> variable whose runtime
/// value is, for example, <see cref="ResizeServerAction"/>. The BinaryLane API
/// requires the concrete action properties as well, so this converter writes
/// the runtime payload and handles all documented discriminator values. Unknown
/// action values are preserved through <see cref="UnknownServerAction"/>.
/// </remarks>
public sealed class ServerActionJsonConverter : JsonConverter<ServerAction>
{
    private static readonly PropertyInfo AdditionalPropertiesProperty =
        typeof(BinaryLaneDto).GetProperty(nameof(BinaryLaneDto.AdditionalProperties))
        ?? throw new InvalidOperationException("BinaryLaneDto must expose extension data.");

    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert) => typeof(ServerAction).IsAssignableFrom(typeToConvert);

    /// <inheritdoc />
    public override ServerAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument document = JsonDocument.ParseValue(ref reader);
        if (document.RootElement.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        if (document.RootElement.ValueKind != JsonValueKind.Object)
        {
            throw new JsonException("A BinaryLane server action must be a JSON object.");
        }

        string type = ReadType(document.RootElement);
        ServerAction action = CreateAction(type);
        PopulateAction(action, document.RootElement, options);
        return action;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ServerAction value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(ServerActionJsonNames.Type, value.Type);

        var writtenProperties = new HashSet<string>(StringComparer.Ordinal)
        {
            ServerActionJsonNames.Type,
        };

        foreach (PropertyInfo property in GetPayloadProperties(value.GetType()))
        {
            string jsonName = GetJsonName(property);
            if (!writtenProperties.Add(jsonName))
            {
                continue;
            }

            object? propertyValue = property.GetValue(value);
            if (propertyValue is null && options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull)
            {
                continue;
            }

            writer.WritePropertyName(jsonName);
            JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
        }

        if (value.AdditionalProperties is not null)
        {
            foreach (KeyValuePair<string, JsonElement> property in value.AdditionalProperties)
            {
                if (!writtenProperties.Add(property.Key))
                {
                    continue;
                }

                writer.WritePropertyName(property.Key);
                property.Value.WriteTo(writer);
            }
        }

        writer.WriteEndObject();
    }

    private static string ReadType(JsonElement action)
    {
        if (!action.TryGetProperty(ServerActionJsonNames.Type, out JsonElement typeElement) ||
            typeElement.ValueKind != JsonValueKind.String ||
            string.IsNullOrWhiteSpace(typeElement.GetString()))
        {
            throw new JsonException("A BinaryLane server action requires a non-empty 'type' discriminator.");
        }

        return typeElement.GetString()!;
    }

    private static ServerAction CreateAction(string type) => type switch
    {
        BinaryLaneValues.ServerActionType.AddDisk => new AddDiskServerAction(),
        BinaryLaneValues.ServerActionType.AttachBackup => new AttachBackupServerAction(),
        BinaryLaneValues.ServerActionType.ChangeAdvancedFeatures => new ChangeAdvancedFeaturesServerAction(),
        BinaryLaneValues.ServerActionType.ChangeAdvancedFirewallRules => new ChangeAdvancedFirewallRulesServerAction(),
        BinaryLaneValues.ServerActionType.ChangeBackupSchedule => new ChangeBackupScheduleServerAction(),
        BinaryLaneValues.ServerActionType.ChangeIpv6 => new ChangeIpv6ServerAction(),
        BinaryLaneValues.ServerActionType.ChangeIpv6ReverseNameservers => new ChangeIpv6ReverseNameserversServerAction(),
        BinaryLaneValues.ServerActionType.ChangeKernel => new ChangeKernelServerAction(),
        BinaryLaneValues.ServerActionType.ChangeManageOffsiteBackupCopies => new ChangeManageOffsiteBackupCopiesServerAction(),
        BinaryLaneValues.ServerActionType.ChangeNetwork => new ChangeNetworkServerAction(),
        BinaryLaneValues.ServerActionType.ChangeOffsiteBackupLocation => new ChangeOffsiteBackupLocationServerAction(),
        BinaryLaneValues.ServerActionType.ChangePartner => new ChangePartnerServerAction(),
        BinaryLaneValues.ServerActionType.ChangePortBlocking => new ChangePortBlockingServerAction(),
        BinaryLaneValues.ServerActionType.ChangeRegion => new ChangeRegionServerAction(),
        BinaryLaneValues.ServerActionType.ChangeReverseName => new ChangeReverseNameServerAction(),
        BinaryLaneValues.ServerActionType.ChangeSeparatePrivateNetworkInterface => new ChangeSeparatePrivateNetworkInterfaceServerAction(),
        BinaryLaneValues.ServerActionType.ChangeSourceAndDestinationCheck => new ChangeSourceAndDestinationCheckServerAction(),
        BinaryLaneValues.ServerActionType.ChangeThresholdAlerts => new ChangeThresholdAlertsServerAction(),
        BinaryLaneValues.ServerActionType.ChangeVpcIpv4 => new ChangeVpcIpv4ServerAction(),
        BinaryLaneValues.ServerActionType.CloneUsingBackup => new CloneUsingBackupServerAction(),
        BinaryLaneValues.ServerActionType.DeleteDisk => new DeleteDiskServerAction(),
        BinaryLaneValues.ServerActionType.DetachBackup => new DetachBackupServerAction(),
        BinaryLaneValues.ServerActionType.DisableBackups => new DisableBackupsServerAction(),
        BinaryLaneValues.ServerActionType.DisableSelinux => new DisableSelinuxServerAction(),
        BinaryLaneValues.ServerActionType.EnableBackups => new EnableBackupsServerAction(),
        BinaryLaneValues.ServerActionType.EnableIpv6 => new EnableIpv6ServerAction(),
        BinaryLaneValues.ServerActionType.IsRunning => new IsRunningServerAction(),
        BinaryLaneValues.ServerActionType.PasswordReset => new PasswordResetServerAction(),
        BinaryLaneValues.ServerActionType.Ping => new PingServerAction(),
        BinaryLaneValues.ServerActionType.PowerCycle => new PowerCycleServerAction(),
        BinaryLaneValues.ServerActionType.PowerOff => new PowerOffServerAction(),
        BinaryLaneValues.ServerActionType.PowerOn => new PowerOnServerAction(),
        BinaryLaneValues.ServerActionType.Reboot => new RebootServerAction(),
        BinaryLaneValues.ServerActionType.Rebuild => new RebuildServerAction(),
        BinaryLaneValues.ServerActionType.Rename => new RenameServerAction(),
        BinaryLaneValues.ServerActionType.Resize => new ResizeServerAction(),
        BinaryLaneValues.ServerActionType.ResizeDisk => new ResizeDiskServerAction(),
        BinaryLaneValues.ServerActionType.Restore => new RestoreServerAction(),
        BinaryLaneValues.ServerActionType.Shutdown => new ShutdownServerAction(),
        BinaryLaneValues.ServerActionType.TakeBackup => new TakeBackupServerAction(),
        BinaryLaneValues.ServerActionType.Uncancel => new UncancelServerAction(),
        BinaryLaneValues.ServerActionType.Uptime => new UptimeServerAction(),
        _ => new UnknownServerAction(type),
    };

    private static void PopulateAction(ServerAction action, JsonElement source, JsonSerializerOptions options)
    {
        var knownProperties = new HashSet<string>(StringComparer.Ordinal)
        {
            ServerActionJsonNames.Type,
        };

        foreach (PropertyInfo property in GetPayloadProperties(action.GetType()))
        {
            string jsonName = GetJsonName(property);
            knownProperties.Add(jsonName);
            if (!source.TryGetProperty(jsonName, out JsonElement value))
            {
                continue;
            }

            object? deserialized = JsonSerializer.Deserialize(value.GetRawText(), property.PropertyType, options);
            property.SetValue(action, deserialized);
        }

        var additionalProperties = new Dictionary<string, JsonElement>(StringComparer.Ordinal);
        foreach (JsonProperty property in source.EnumerateObject())
        {
            if (!knownProperties.Contains(property.Name))
            {
                additionalProperties[property.Name] = property.Value.Clone();
            }
        }

        if (additionalProperties.Count > 0)
        {
            AdditionalPropertiesProperty.SetValue(action, additionalProperties);
        }
    }

    private static IEnumerable<PropertyInfo> GetPayloadProperties(Type actionType)
    {
        foreach (PropertyInfo property in actionType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!property.CanRead || !property.CanWrite ||
                property.Name == nameof(ServerAction.Type) ||
                property.Name == nameof(BinaryLaneDto.AdditionalProperties) ||
                property.GetIndexParameters().Length != 0 ||
                property.GetCustomAttribute<JsonIgnoreAttribute>()?.Condition == JsonIgnoreCondition.Always)
            {
                continue;
            }

            yield return property;
        }
    }

    private static string GetJsonName(PropertyInfo property) =>
        property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name;
}
