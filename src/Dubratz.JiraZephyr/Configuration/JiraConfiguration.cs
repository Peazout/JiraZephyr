namespace Dubratz.JiraZephyr.Configuration;

/// <summary>
/// Configuration for connecting to Jira (Cloud or On-Premise)
/// </summary>
public class JiraConfiguration
{
    /// <summary>
    /// Base URL of the Jira instance (e.g., "https://yourcompany.atlassian.net" for Cloud or "https://jira.yourcompany.com" for On-Premise)
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Username or email for authentication
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password for Basic Auth (On-Premise) or API Token (Cloud)
    /// </summary>
    public string ApiToken { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if this is a Jira Cloud instance (true) or On-Premise (false)
    /// </summary>
    public bool IsCloud { get; set; } = true;

    /// <summary>
    /// Optional: Personal Access Token (PAT) for authentication (alternative to Username/ApiToken)
    /// </summary>
    public string? PersonalAccessToken { get; set; }

    /// <summary>
    /// Timeout for HTTP requests in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Validates the configuration
    /// </summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
            throw new ArgumentException("BaseUrl is required", nameof(BaseUrl));

        if (string.IsNullOrWhiteSpace(PersonalAccessToken))
        {
            if (string.IsNullOrWhiteSpace(Username))
                throw new ArgumentException("Username is required when PersonalAccessToken is not provided", nameof(Username));

            if (string.IsNullOrWhiteSpace(ApiToken))
                throw new ArgumentException("ApiToken is required when PersonalAccessToken is not provided", nameof(ApiToken));
        }
    }
}
