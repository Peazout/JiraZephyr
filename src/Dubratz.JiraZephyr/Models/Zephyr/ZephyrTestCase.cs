using System.Text.Json.Serialization;


namespace Dubratz.JiraZephyr.Models.Zephyr
{

    public enum ZephyrTestScriptType
    {
        PLAIN_TEXT,
        STEP_BY_STEP,
        BDD
    }

    public class ZephyrTestScript
    {
        public long? id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ZephyrTestScriptType type { get; set; }

        public List<ZephyrTestStep> steps { get; set; }

        public ZephyrTestScript()
        {
            steps = new List<ZephyrTestStep>();
            type = ZephyrTestScriptType.STEP_BY_STEP;
        }

    }

    public class ZephyrTestStep
    {
        public string expectedResult { get; set; }
        public int? index { get; set; }
        public string description { get; set; }
        public long? id { get; set; }
        public string testData { get; set; }
    }

    public class Parameters
    {
        public List<string> variables { get; set; }
        public List<string> entries { get; set; }

        public Parameters()
        {
            variables = new List<string>();
            entries = new List<string>();
        }
    }

    public class ZephyrTestCase
    {
        public string key { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string owner { get; set; }
        public string priority { get; set; }
        public int? majorVersion { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? updatedOn { get; set; }
        public string updatedBy { get; set; }
        public long estimatedTime { get; set; }
        public string lastTestResultStatus { get; set; }
        public string objective { get; set; }
        public string precondition { get; set; }
        public string projectKey { get; set; }
        public string folder { get; set; }
        public string createdBy { get; set; }
        public bool? latestVersion { get; set; }
        public List<string> labels { get; set; }
        public ZephyrTestScript testScript { get; set; }
        public Parameters parameters { get; set; }

        public ZephyrTestCase()
        {
            labels = new List<string>();

            testScript = new ZephyrTestScript();

        }

    }

}
