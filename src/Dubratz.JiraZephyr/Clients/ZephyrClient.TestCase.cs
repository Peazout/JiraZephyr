using Dubratz.JiraZephyr.Models.Zephyr;
using System.Net.Http.Json;


namespace Dubratz.JiraZephyr.Clients
{
    public partial class ZephyrClient
    {

        /// <summary>
        /// Get testcase case object from Jira Zephyr by test case ID. The test case ID is the unique identifier for the test case in Zephyr, which can be obtained from the test case details page or from the list of test cases in a project.
        /// </summary>
        public async Task<ZephyrTestCase?> GetTestCaseAsync(string testCaseId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(testCaseId))
                throw new ArgumentException("Test case ID cannot be null or empty", nameof(testCaseId));

            var response = await _httpClient.GetAsync($"testcase/{testCaseId}", cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ZephyrTestCase>(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Post a new test case to Jira Zephyr. The project ID is the unique identifier for the project in Zephyr, which can be obtained from the project details page or from the list of projects. The name is the name of the test case, and the objective is an optional description of the test case's purpose or goal.
        /// </summary>
        public async Task<ZephyrTestCase?> PostTestCaseAsync(long projectId, string name, string? objective = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Test case name cannot be null or empty", nameof(name));

            var requestBody = new
            {
                projectId,
                name,
                objective
            };

            var response = await _httpClient.PostAsJsonAsync("testcase", requestBody, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ZephyrTestCase>(cancellationToken: cancellationToken);

        }


        /// <summary>
        /// Delete a test case from Jira Zephyr. The project ID is the unique identifier for the project in Zephyr, which can be obtained from the project details page or from the list of projects. The name is the name of the test case, and the objective is an optional description of the test case's purpose or goal.
        /// </summary>
        public async Task DeleteTestCaseAsync(string testCaseKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(testCaseKey))
                throw new ArgumentException("Test case name cannot be null or empty", nameof(testCaseKey));

            var response = await _httpClient.DeleteAsync($"testcase/{testCaseKey}", cancellationToken);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadFromJsonAsync<ZephyrTestCase>(cancellationToken: cancellationToken);
            return;

        }


    }

}
