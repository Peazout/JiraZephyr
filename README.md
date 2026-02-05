# JiraZephyr

A .NET Core C# library for connecting to Jira and the Zephyr Scale (formerly Zephyr for Jira) test management add-on. Supports both **Cloud** and **On-Premise** versions of Jira.

## Features

- ✅ Full support for Jira REST API (Cloud & On-Premise)
- ✅ Full support for Zephyr Scale REST API (Cloud & On-Premise)
- ✅ Multiple authentication methods (Basic Auth, API Token, Personal Access Token)
- ✅ Comprehensive models for Jira issues, projects, and users
- ✅ Comprehensive models for Zephyr test cases, test executions, and test cycles
- ✅ Async/await support for all operations
- ✅ Built with .NET 8.0
- ✅ Fully testable with dependency injection support

## Installation

### Via NuGet Package Manager

```bash
dotnet add package JiraZephyr
```

Or using the Package Manager Console in Visual Studio:

```powershell
Install-Package JiraZephyr
```

### From Source

```bash
# Clone the repository
git clone https://github.com/Peazout/JiraZephyr.git

# Build the solution
cd JiraZephyr
dotnet build

# Run tests
dotnet test
```

## Usage

### Jira Client

#### Cloud Configuration

```csharp
using JiraZephyr.Configuration;
using JiraZephyr.Clients;

// Configure for Jira Cloud with API Token
var config = new JiraConfiguration
{
    BaseUrl = "https://yourcompany.atlassian.net",
    Username = "your-email@example.com",
    ApiToken = "your-api-token",
    IsCloud = true
};

// Create the client
using var jiraClient = new JiraClient(config);

// Get an issue
var issue = await jiraClient.GetIssueAsync("PROJ-123");
Console.WriteLine($"Issue: {issue?.Key} - {issue?.Fields?.Summary}");

// Search issues using JQL
var issues = await jiraClient.SearchIssuesAsync("project = PROJ AND status = Open");
foreach (var item in issues)
{
    Console.WriteLine($"{item.Key}: {item.Fields?.Summary}");
}

// Create a new issue
var newIssue = await jiraClient.CreateIssueAsync(
    projectKey: "PROJ",
    issueType: "Task",
    summary: "New task from API",
    description: "This is a test issue created via the API"
);
```

#### On-Premise Configuration

```csharp
// Configure for Jira On-Premise with Basic Auth
var config = new JiraConfiguration
{
    BaseUrl = "https://jira.yourcompany.com",
    Username = "your-username",
    ApiToken = "your-password",  // Use password for on-premise
    IsCloud = false
};

using var jiraClient = new JiraClient(config);
```

#### Personal Access Token (PAT)

```csharp
// Configure with Personal Access Token
var config = new JiraConfiguration
{
    BaseUrl = "https://yourcompany.atlassian.net",
    PersonalAccessToken = "your-pat-token",
    IsCloud = true
};

using var jiraClient = new JiraClient(config);
```

### Zephyr Client

#### Cloud Configuration

```csharp
using JiraZephyr.Configuration;
using JiraZephyr.Clients;

// Configure for Zephyr Scale Cloud
var config = new ZephyrConfiguration
{
    ApiToken = "your-zephyr-api-token",
    IsCloud = true  // BaseUrl will be set automatically
};

// Create the client
using var zephyrClient = new ZephyrClient(config);

// Get a test case
var testCase = await zephyrClient.GetTestCaseAsync("TEST-123");
Console.WriteLine($"Test Case: {testCase?.Name}");

// Create a test case
var newTestCase = await zephyrClient.CreateTestCaseAsync(
    projectId: 10000,
    name: "Test user login",
    objective: "Verify that users can log in successfully"
);

// Create a test execution
var execution = await zephyrClient.CreateTestExecutionAsync(
    projectId: 10000,
    testCaseId: "TEST-123",
    testCycleId: "CYCLE-1",
    statusName: "Pass"
);

// Update test execution status
await zephyrClient.UpdateTestExecutionStatusAsync(
    executionId: execution!.Id!,
    statusName: "Fail",
    comment: "Login button not responding"
);
```

#### On-Premise Configuration

```csharp
// Configure for Zephyr Scale On-Premise
var config = new ZephyrConfiguration
{
    BaseUrl = "https://jira.yourcompany.com/rest/atm/1.0",
    ApiToken = "your-api-token",
    IsCloud = false
};

using var zephyrClient = new ZephyrClient(config);
```

## API Reference

### Jira Client Methods

- `GetIssueAsync(issueKey)` - Get a Jira issue by key
- `GetProjectAsync(projectKey)` - Get a Jira project by key
- `CreateIssueAsync(projectKey, issueType, summary, description)` - Create a new issue
- `UpdateIssueAsync(issueKey, fields)` - Update an issue
- `SearchIssuesAsync(jql, maxResults, startAt)` - Search issues using JQL

### Zephyr Client Methods

- `GetTestCaseAsync(testCaseId)` - Get a test case
- `CreateTestCaseAsync(projectId, name, objective)` - Create a test case
- `GetTestExecutionAsync(executionId)` - Get a test execution
- `CreateTestExecutionAsync(projectId, testCaseId, testCycleId, statusName)` - Create a test execution
- `UpdateTestExecutionStatusAsync(executionId, statusName, comment)` - Update execution status
- `GetTestExecutionsAsync(testCycleId, maxResults, startAt)` - Get test executions for a cycle
- `GetTestCycleAsync(testCycleId)` - Get a test cycle
- `CreateTestCycleAsync(projectId, name, description)` - Create a test cycle

## Authentication

### Jira Cloud
- **API Token**: Generate from [Atlassian Account Settings](https://id.atlassian.com/manage-profile/security/api-tokens)
- **Personal Access Token (PAT)**: Generate from Jira Cloud settings

### Jira On-Premise
- **Basic Auth**: Use username and password
- **Personal Access Token**: Available in newer versions

### Zephyr Scale
- **API Token**: Generate from Zephyr Scale settings in Jira

## Project Structure

```
JiraZephyr/
├── src/
│   └── JiraZephyr/
│       ├── Clients/           # API client implementations
│       ├── Configuration/     # Configuration classes
│       ├── Interfaces/        # Client interfaces
│       └── Models/            # Data models
│           ├── Jira/         # Jira models
│           └── Zephyr/       # Zephyr models
└── tests/
    └── JiraZephyr.Tests/     # Unit tests
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Resources

- [Jira Cloud REST API](https://developer.atlassian.com/cloud/jira/platform/rest/v2/)
- [Jira Server REST API](https://docs.atlassian.com/software/jira/docs/api/REST/latest/)
- [Zephyr Scale Cloud API](https://support.smartbear.com/zephyr-scale-cloud/api-docs/)
- [Zephyr Scale Server API](https://support.smartbear.com/zephyr-scale-server/api-docs/)
