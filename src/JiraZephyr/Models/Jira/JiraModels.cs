using System.Text.Json.Serialization;

namespace JiraZephyr.Models.Jira;

/// <summary>
/// Represents a Jira issue
/// </summary>
public class JiraIssue
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("fields")]
    public JiraIssueFields? Fields { get; set; }
}

/// <summary>
/// Represents the fields of a Jira issue
/// </summary>
public class JiraIssueFields
{
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("description")]
    public object? Description { get; set; }

    [JsonPropertyName("status")]
    public JiraStatus? Status { get; set; }

    [JsonPropertyName("priority")]
    public JiraPriority? Priority { get; set; }

    [JsonPropertyName("issuetype")]
    public JiraIssueType? IssueType { get; set; }

    [JsonPropertyName("project")]
    public JiraProject? Project { get; set; }

    [JsonPropertyName("assignee")]
    public JiraUser? Assignee { get; set; }

    [JsonPropertyName("reporter")]
    public JiraUser? Reporter { get; set; }

    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [JsonPropertyName("updated")]
    public DateTime? Updated { get; set; }
}

/// <summary>
/// Represents a Jira status
/// </summary>
public class JiraStatus
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

/// <summary>
/// Represents a Jira priority
/// </summary>
public class JiraPriority
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

/// <summary>
/// Represents a Jira issue type
/// </summary>
public class JiraIssueType
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

/// <summary>
/// Represents a Jira user
/// </summary>
public class JiraUser
{
    [JsonPropertyName("accountId")]
    public string? AccountId { get; set; }

    [JsonPropertyName("emailAddress")]
    public string? EmailAddress { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

/// <summary>
/// Represents a Jira project
/// </summary>
public class JiraProject
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
