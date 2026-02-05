using Dubratz.JiraZephyr.Models.Zephyr;

namespace Dubratz.JiraZephyr.Interfaces;

/// <summary>
/// Interface for Zephyr client operations
/// </summary>
public interface IZephyrClient
{
    /// <summary>
    /// Gets a test case by its ID
    /// </summary>
    /// <param name="testCaseId">The test case ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The test case</returns>
    Task<ZephyrTestCase?> GetTestCaseAsync(string testCaseId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new test case
    /// </summary>
    /// <param name="projectId">The project ID</param>
    /// <param name="name">The test case name</param>
    /// <param name="objective">The test case objective</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created test case</returns>
    Task<ZephyrTestCase?> PostTestCaseAsync(long projectId, string name, string? objective = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a test case by its key
    /// </summary>
    /// <param name="testCaseKey">Testcase ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task DeleteTestCaseAsync(string testCaseKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a test execution by its ID
    /// </summary>
    /// <param name="executionId">The execution ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The test execution</returns>
    Task<ZephyrTestExecution?> GetTestExecutionAsync(string executionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new test execution
    /// </summary>
    /// <param name="projectId">The project ID</param>
    /// <param name="testCaseId">The test case ID</param>
    /// <param name="testCycleId">The test cycle ID</param>
    /// <param name="statusName">The execution status name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created test execution</returns>
    Task<ZephyrTestExecution?> CreateTestExecutionAsync(long projectId, string testCaseId, string? testCycleId = null, string? statusName = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a test execution status
    /// </summary>
    /// <param name="executionId">The execution ID</param>
    /// <param name="statusName">The new status name</param>
    /// <param name="comment">Optional comment</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    Task<bool> UpdateTestExecutionStatusAsync(string executionId, string statusName, string? comment = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets test executions for a test cycle
    /// </summary>
    /// <param name="testCycleId">The test cycle ID</param>
    /// <param name="maxResults">Maximum number of results</param>
    /// <param name="startAt">Starting index for pagination</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paged response of test executions</returns>
    Task<ZephyrPagedResponse<ZephyrTestExecution>?> GetTestExecutionsAsync(string testCycleId, int maxResults = 50, int startAt = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a test cycle by its ID
    /// </summary>
    /// <param name="testCycleId">The test cycle ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The test cycle</returns>
    Task<ZephyrTestCycle?> GetTestCycleAsync(string testCycleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new test cycle
    /// </summary>
    /// <param name="projectId">The project ID</param>
    /// <param name="name">The test cycle name</param>
    /// <param name="description">The test cycle description</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created test cycle</returns>
    Task<ZephyrTestCycle?> CreateTestCycleAsync(long projectId, string name, string? description = null, CancellationToken cancellationToken = default);
}
