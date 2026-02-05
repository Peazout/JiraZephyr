using JiraZephyr.Configuration;
using Xunit;

namespace JiraZephyr.Tests.Configuration;

public class ZephyrConfigurationTests
{
    [Fact]
    public void Validate_WithValidConfiguration_ShouldNotThrow()
    {
        // Arrange
        var config = new ZephyrConfiguration
        {
            BaseUrl = "https://api.zephyrscale.smartbear.com/v2",
            ApiToken = "test-token",
            IsCloud = true
        };

        // Act & Assert
        var exception = Record.Exception(() => config.Validate());
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithEmptyApiToken_ShouldThrow()
    {
        // Arrange
        var config = new ZephyrConfiguration
        {
            BaseUrl = "https://api.zephyrscale.smartbear.com/v2",
            ApiToken = "",
            IsCloud = true
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => config.Validate());
    }

    [Fact]
    public void Validate_CloudWithoutBaseUrl_ShouldSetDefault()
    {
        // Arrange
        var config = new ZephyrConfiguration
        {
            ApiToken = "test-token",
            IsCloud = true
        };

        // Act
        config.Validate();

        // Assert
        Assert.Equal("https://api.zephyrscale.smartbear.com/v2", config.BaseUrl);
    }
}
