using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using JiraZephyr.Configuration;
using JiraZephyr.Interfaces;
using JiraZephyr.Models.Jira;

namespace JiraZephyr.Clients;

/// <summary>
/// Client for interacting with Jira REST API (Cloud and On-Premise)
/// </summary>
public class JiraClient : IJiraClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly JiraConfiguration _configuration;
    private readonly bool _disposeHttpClient;

    /// <summary>
    /// Initializes a new instance of the JiraClient class with a configuration
    /// </summary>
    /// <param name="configuration">The Jira configuration</param>
    public JiraClient(JiraConfiguration configuration)
        : this(configuration, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the JiraClient class with a configuration and HttpClient
    /// </summary>
    /// <param name="configuration">The Jira configuration</param>
    /// <param name="httpClient">Optional HttpClient instance. If not provided, a new one will be created.</param>
    public JiraClient(JiraConfiguration configuration, HttpClient? httpClient)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _configuration.Validate();

        if (httpClient == null)
        {
            _httpClient = new HttpClient();
            _disposeHttpClient = true;
        }
        else
        {
            _httpClient = httpClient;
            _disposeHttpClient = false;
        }

        _httpClient.BaseAddress = new Uri(_configuration.BaseUrl.TrimEnd('/') + "/rest/api/2/");
        _httpClient.Timeout = TimeSpan.FromSeconds(_configuration.TimeoutSeconds);

        SetupAuthentication();
    }

    private void SetupAuthentication()
    {
        if (!string.IsNullOrWhiteSpace(_configuration.PersonalAccessToken))
        {
            // Use Personal Access Token (PAT)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.PersonalAccessToken);
        }
        else
        {
            // Use Basic Authentication (username + API token for Cloud, username + password for On-Premise)
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_configuration.Username}:{_configuration.ApiToken}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <inheritdoc/>
    public async Task<JiraIssue?> GetIssueAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(issueKey))
            throw new ArgumentException("Issue key cannot be null or empty", nameof(issueKey));

        var response = await _httpClient.GetAsync($"issue/{issueKey}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<JiraIssue>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<JiraProject?> GetProjectAsync(string projectKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(projectKey))
            throw new ArgumentException("Project key cannot be null or empty", nameof(projectKey));

        var response = await _httpClient.GetAsync($"project/{projectKey}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<JiraProject>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<JiraIssue?> CreateIssueAsync(string projectKey, string issueType, string summary, string? description = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(projectKey))
            throw new ArgumentException("Project key cannot be null or empty", nameof(projectKey));
        if (string.IsNullOrWhiteSpace(issueType))
            throw new ArgumentException("Issue type cannot be null or empty", nameof(issueType));
        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Summary cannot be null or empty", nameof(summary));

        var requestBody = new
        {
            fields = new Dictionary<string, object>
            {
                ["project"] = new { key = projectKey },
                ["issuetype"] = new { name = issueType },
                ["summary"] = summary
            }
        };

        if (!string.IsNullOrWhiteSpace(description))
        {
            requestBody.fields["description"] = description;
        }

        var response = await _httpClient.PostAsJsonAsync("issue", requestBody, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<JiraIssue>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateIssueAsync(string issueKey, Dictionary<string, object> fields, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(issueKey))
            throw new ArgumentException("Issue key cannot be null or empty", nameof(issueKey));
        if (fields == null || fields.Count == 0)
            throw new ArgumentException("Fields cannot be null or empty", nameof(fields));

        var requestBody = new { fields };

        var response = await _httpClient.PutAsJsonAsync($"issue/{issueKey}", requestBody, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    /// <inheritdoc/>
    public async Task<List<JiraIssue>> SearchIssuesAsync(string jql, int maxResults = 50, int startAt = 0, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jql))
            throw new ArgumentException("JQL cannot be null or empty", nameof(jql));

        var requestBody = new
        {
            jql,
            maxResults,
            startAt
        };

        var response = await _httpClient.PostAsJsonAsync("search", requestBody, cancellationToken);
        response.EnsureSuccessStatusCode();

        var searchResult = await response.Content.ReadFromJsonAsync<JsonDocument>(cancellationToken: cancellationToken);
        if (searchResult == null)
            return new List<JiraIssue>();

        var issuesElement = searchResult.RootElement.GetProperty("issues");
        var issues = JsonSerializer.Deserialize<List<JiraIssue>>(issuesElement.GetRawText());

        return issues ?? new List<JiraIssue>();
    }

    /// <summary>
    /// Disposes the HttpClient if it was created by this instance
    /// </summary>
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient.Dispose();
        }
    }
}
