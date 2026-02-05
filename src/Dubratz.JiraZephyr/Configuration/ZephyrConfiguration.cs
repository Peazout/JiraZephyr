namespace Dubratz.JiraZephyr.Configuration;

/// <summary>
/// Configuration for connecting to Zephyr Scale (Cloud or On-Premise)
/// </summary>
public class ZephyrConfiguration
{
    /// <summary>
    /// API Token for Zephyr Scale authentication
    /// </summary>
    public string ApiToken { get; set; } = string.Empty;

    /// <summary>
    /// Base URL for Zephyr Scale API (e.g., "https://api.zephyrscale.smartbear.com/v2" for Cloud)
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Indicates if this is a Zephyr Scale Cloud instance (true) or On-Premise (false)
    /// </summary>
    public bool IsCloud { get; set; } = true;

    /// <summary>
    /// Validates the configuration
    /// </summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiToken))
            throw new ArgumentException("ApiToken is required", nameof(ApiToken));

        if (IsCloud && string.IsNullOrWhiteSpace(BaseUrl))
            BaseUrl = "https://api.zephyrscale.smartbear.com/v2";
    }
}
