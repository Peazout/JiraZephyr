using System.Text.Json.Serialization;

namespace JiraZephyr.Models.Zephyr;

/// <summary>
/// Represents a Zephyr test case
/// </summary>
public class ZephyrTestCase
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("objective")]
    public string? Objective { get; set; }

    [JsonPropertyName("precondition")]
    public string? Precondition { get; set; }

    [JsonPropertyName("projectId")]
    public long? ProjectId { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("folderId")]
    public long? FolderId { get; set; }

    [JsonPropertyName("createdOn")]
    public DateTime? CreatedOn { get; set; }

    [JsonPropertyName("updatedOn")]
    public DateTime? UpdatedOn { get; set; }
}

/// <summary>
/// Represents a Zephyr test execution
/// </summary>
public class ZephyrTestExecution
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("testCaseId")]
    public string? TestCaseId { get; set; }

    [JsonPropertyName("testCycleId")]
    public string? TestCycleId { get; set; }

    [JsonPropertyName("projectId")]
    public long? ProjectId { get; set; }

    [JsonPropertyName("status")]
    public ZephyrExecutionStatus? Status { get; set; }

    [JsonPropertyName("executedById")]
    public string? ExecutedById { get; set; }

    [JsonPropertyName("assignedToId")]
    public string? AssignedToId { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("executedOn")]
    public DateTime? ExecutedOn { get; set; }

    [JsonPropertyName("createdOn")]
    public DateTime? CreatedOn { get; set; }
}

/// <summary>
/// Represents a Zephyr test cycle
/// </summary>
public class ZephyrTestCycle
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("projectId")]
    public long? ProjectId { get; set; }

    [JsonPropertyName("plannedStartDate")]
    public DateTime? PlannedStartDate { get; set; }

    [JsonPropertyName("plannedEndDate")]
    public DateTime? PlannedEndDate { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("createdOn")]
    public DateTime? CreatedOn { get; set; }

    [JsonPropertyName("updatedOn")]
    public DateTime? UpdatedOn { get; set; }
}

/// <summary>
/// Represents a Zephyr execution status
/// </summary>
public class ZephyrExecutionStatus
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// Represents a response with pagination
/// </summary>
/// <typeparam name="T">The type of items in the response</typeparam>
public class ZephyrPagedResponse<T>
{
    [JsonPropertyName("values")]
    public List<T>? Values { get; set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; set; }

    [JsonPropertyName("startAt")]
    public int StartAt { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("isLast")]
    public bool IsLast { get; set; }
}
