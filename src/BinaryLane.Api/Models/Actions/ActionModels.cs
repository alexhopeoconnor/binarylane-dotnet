using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BinaryLane.Api.V2.Models;

/// <summary>A provider action, which may still be in progress.</summary>
public sealed class BinaryLaneAction : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>Provider status, for example <c>in-progress</c>, <c>completed</c>, or <c>errored</c>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("started_at")]
    public DateTimeOffset StartedAt { get; init; }

    [JsonPropertyName("completed_at")]
    public DateTimeOffset? CompletedAt { get; init; }

    /// <summary>Provider resource type, when the action is associated with one.</summary>
    [JsonPropertyName("resource_type")]
    public string? ResourceType { get; init; }

    [JsonPropertyName("resource_id")]
    public long? ResourceId { get; init; }

    [JsonPropertyName("region")]
    public Region? Region { get; init; }

    [JsonPropertyName("region_slug")]
    public string? RegionSlug { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("reason")]
    public string Reason { get; init; } = string.Empty;

    [JsonPropertyName("progress")]
    public ActionProgress Progress { get; init; } = new();

    [JsonPropertyName("result_data")]
    public string? ResultData { get; init; }

    [JsonPropertyName("blocking_invoice_id")]
    public long? BlockingInvoiceId { get; init; }

    [JsonPropertyName("user_interaction_required")]
    public UserInteractionRequired? UserInteractionRequired { get; init; }
}

/// <summary>Progress information for a long-running provider action.</summary>
public sealed class ActionProgress : BinaryLaneDto
{
    [JsonPropertyName("current_step_detail")]
    public string? CurrentStepDetail { get; init; }

    [JsonPropertyName("percent_complete")]
    public int PercentComplete { get; init; }

    [JsonPropertyName("current_step")]
    public string? CurrentStep { get; init; }

    [JsonPropertyName("completed_steps")]
    public IReadOnlyList<string> CompletedSteps { get; init; } = Array.Empty<string>();
}

/// <summary>A link to an action related to a completed request.</summary>
public sealed class ActionLink : BinaryLaneDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("rel")]
    public string Rel { get; init; } = string.Empty;

    [JsonPropertyName("href")]
    public string Href { get; init; } = string.Empty;
}

/// <summary>Indicates that an action needs an explicit user decision.</summary>
public sealed class UserInteractionRequired : BinaryLaneDto
{
    /// <summary>Provider interaction type, such as <c>continue-after-ping-failure</c>.</summary>
    [JsonPropertyName("interaction_type")]
    public string InteractionType { get; init; } = string.Empty;
}

public sealed class ActionResponse : BinaryLaneDto
{
    [JsonPropertyName("action")]
    public BinaryLaneAction Action { get; init; } = new();
}

public sealed class ActionsLinks : BinaryLaneDto
{
    [JsonPropertyName("actions")]
    public IReadOnlyList<ActionLink> Actions { get; init; } = Array.Empty<ActionLink>();
}

public sealed class ActionsResponse : BinaryLaneDto
{
    [JsonPropertyName("meta")]
    public PageMeta Meta { get; init; } = new();

    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }

    [JsonPropertyName("actions")]
    public IReadOnlyList<BinaryLaneAction> Actions { get; init; } = Array.Empty<BinaryLaneAction>();
}

/// <summary>Allows or declines the interaction requested by an action.</summary>
public sealed class ProceedRequest : BinaryLaneRequestModel
{
    [JsonPropertyName("proceed")]
    public bool Proceed { get; init; }
}
