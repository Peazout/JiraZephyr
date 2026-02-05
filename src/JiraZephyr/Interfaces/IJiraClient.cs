using JiraZephyr.Models.Jira;

namespace JiraZephyr.Interfaces;

/// <summary>
/// Interface for Jira client operations
/// </summary>
public interface IJiraClient
{
    /// <summary>
    /// Gets a Jira issue by its key
    /// </summary>
    /// <param name="issueKey">The issue key (e.g., "PROJ-123")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Jira issue</returns>
    Task<JiraIssue?> GetIssueAsync(string issueKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a Jira project by its key
    /// </summary>
    /// <param name="projectKey">The project key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Jira project</returns>
    Task<JiraProject?> GetProjectAsync(string projectKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new Jira issue
    /// </summary>
    /// <param name="projectKey">The project key</param>
    /// <param name="issueType">The issue type</param>
    /// <param name="summary">The issue summary</param>
    /// <param name="description">The issue description</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created issue</returns>
    Task<JiraIssue?> CreateIssueAsync(string projectKey, string issueType, string summary, string? description = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a Jira issue
    /// </summary>
    /// <param name="issueKey">The issue key</param>
    /// <param name="fields">The fields to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    Task<bool> UpdateIssueAsync(string issueKey, Dictionary<string, object> fields, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for issues using JQL (Jira Query Language)
    /// </summary>
    /// <param name="jql">The JQL query</param>
    /// <param name="maxResults">Maximum number of results to return</param>
    /// <param name="startAt">Starting index for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of issues matching the query</returns>
    Task<List<JiraIssue>> SearchIssuesAsync(string jql, int maxResults = 50, int startAt = 0, CancellationToken cancellationToken = default);
}
