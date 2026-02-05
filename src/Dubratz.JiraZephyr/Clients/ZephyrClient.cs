using System.Net.Http.Headers;
using System.Net.Http.Json;
using Dubratz.JiraZephyr.Configuration;
using Dubratz.JiraZephyr.Interfaces;
using Dubratz.JiraZephyr.Models.Zephyr;

namespace Dubratz.JiraZephyr.Clients;

/// <summary>
/// Client for interacting with Zephyr Scale REST API (Cloud and On-Premise)
/// </summary>
public partial class ZephyrClient : JiraClient, IZephyrClient, IDisposable
{

    /// <summary>
    /// Initializes a new instance of the ZephyrClient class with a configuration
    /// </summary>
    /// <param name="configuration">The Zephyr configuration</param>
    public ZephyrClient(JiraConfiguration configuration)
        : this(configuration, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the ZephyrClient class with a configuration and HttpClient
    /// </summary>
    /// <param name="configuration">The Zephyr configuration</param>
    /// <param name="httpClient">Optional HttpClient instance. If not provided, a new one will be created.</param>
    public ZephyrClient(JiraConfiguration configuration, HttpClient? httpClient) : base(configuration, httpClient)
    {

        if (!configuration.IsCloud)
        {
            _httpClient.BaseAddress = new Uri(_configuration.BaseUrl!.TrimEnd('/') + "/rest/atm/1.0/");
        }
        else
        {
            _httpClient.BaseAddress = new Uri(_configuration.BaseUrl!.TrimEnd('/') + "tm4j/v2/");
        }

    }

    /// <inheritdoc/>
    public async Task<ZephyrTestExecution?> GetTestExecutionAsync(string executionId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(executionId))
            throw new ArgumentException("Execution ID cannot be null or empty", nameof(executionId));

        var response = await _httpClient.GetAsync($"testexecutions/{executionId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ZephyrTestExecution>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ZephyrTestExecution?> CreateTestExecutionAsync(long projectId, string testCaseId, string? testCycleId = null, string? statusName = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(testCaseId))
            throw new ArgumentException("Test case ID cannot be null or empty", nameof(testCaseId));

        var requestBody = new
        {
            projectId,
            testCaseId,
            testCycleId,
            statusName
        };

        var response = await _httpClient.PostAsJsonAsync("testexecutions", requestBody, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ZephyrTestExecution>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateTestExecutionStatusAsync(string executionId, string statusName, string? comment = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(executionId))
            throw new ArgumentException("Execution ID cannot be null or empty", nameof(executionId));
        if (string.IsNullOrWhiteSpace(statusName))
            throw new ArgumentException("Status name cannot be null or empty", nameof(statusName));

        var requestBody = new
        {
            statusName,
            comment
        };

        var response = await _httpClient.PutAsJsonAsync($"testexecutions/{executionId}", requestBody, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    /// <inheritdoc/>
    public async Task<ZephyrPagedResponse<ZephyrTestExecution>?> GetTestExecutionsAsync(string testCycleId, int maxResults = 50, int startAt = 0, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(testCycleId))
            throw new ArgumentException("Test cycle ID cannot be null or empty", nameof(testCycleId));

        var response = await _httpClient.GetAsync($"testcycles/{testCycleId}/testexecutions?maxResults={maxResults}&startAt={startAt}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ZephyrPagedResponse<ZephyrTestExecution>>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ZephyrTestCycle?> GetTestCycleAsync(string testCycleId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(testCycleId))
            throw new ArgumentException("Test cycle ID cannot be null or empty", nameof(testCycleId));

        var response = await _httpClient.GetAsync($"testcycles/{testCycleId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ZephyrTestCycle>(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ZephyrTestCycle?> CreateTestCycleAsync(long projectId, string name, string? description = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Test cycle name cannot be null or empty", nameof(name));

        var requestBody = new
        {
            projectId,
            name,
            description
        };

        var response = await _httpClient.PostAsJsonAsync("testcycles", requestBody, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ZephyrTestCycle>(cancellationToken: cancellationToken);
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
