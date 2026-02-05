using JiraZephyr.Configuration;
using Xunit;

namespace JiraZephyr.Tests.Configuration;

public class JiraConfigurationTests
{
    [Fact]
    public void Validate_WithValidConfiguration_ShouldNotThrow()
    {
        // Arrange
        var config = new JiraConfiguration
        {
            BaseUrl = "https://test.atlassian.net",
            Username = "test@example.com",
            ApiToken = "test-token",
            IsCloud = true
        };

        // Act & Assert
        var exception = Record.Exception(() => config.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithPAT_ShouldNotThrow()
    {
        // Arrange
        var config = new JiraConfiguration
        {
            BaseUrl = "https://test.atlassian.net",
            PersonalAccessToken = "pat-token",
            IsCloud = true
        };

        // Act & Assert
        var exception = Record.Exception(() => config.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithEmptyBaseUrl_ShouldThrow()
    {
        // Arrange
        var config = new JiraConfiguration
        {
            BaseUrl = "",
            Username = "test@example.com",
            ApiToken = "test-token"
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => config.Validate());
    }

    [Fact]
    public void Validate_WithEmptyUsername_ShouldThrow()
    {
        // Arrange
        var config = new JiraConfiguration
        {
            BaseUrl = "https://test.atlassian.net",
            Username = "",
            ApiToken = "test-token"
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => config.Validate());
    }

    [Fact]
    public void Validate_WithEmptyApiToken_ShouldThrow()
    {
        // Arrange
        var config = new JiraConfiguration
        {
            BaseUrl = "https://test.atlassian.net",
            Username = "test@example.com",
            ApiToken = ""
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => config.Validate());
    }
}
