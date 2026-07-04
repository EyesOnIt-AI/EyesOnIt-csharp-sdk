using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIRuleAction
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "alert";

        [JsonPropertyName("destinations")]
        public string[] Destinations { get; set; }

        [JsonPropertyName("include_frame")]
        public bool? IncludeFrame { get; set; }

        [JsonPropertyName("pre_roll_seconds")]
        public float? PreRollSeconds { get; set; }

        [JsonPropertyName("post_roll_seconds")]
        public float? PostRollSeconds { get; set; }

        [JsonPropertyName("parameters")]
        public Dictionary<string, object> Parameters { get; set; }
    }

    public class EOIRuleCondition
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "count";

        [JsonPropertyName("source_config_id")]
        public string SourceConfigId { get; set; }

        [JsonPropertyName("detection_config_id")]
        public string DetectionConfigId { get; set; }

        [JsonPropertyName("primary_config_id")]
        public string PrimaryConfigId { get; set; }

        [JsonPropertyName("secondary_config_id")]
        public string SecondaryConfigId { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("value")]
        public float? Value { get; set; }

        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonPropertyName("alert_direction")]
        public string AlertDirection { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("dwell_seconds")]
        public float? DwellSeconds { get; set; }

        [JsonPropertyName("reset_seconds")]
        public float? ResetSeconds { get; set; }

        [JsonPropertyName("parameters")]
        public Dictionary<string, object> Parameters { get; set; }
    }

    public class EOIRule
    {
        [JsonPropertyName("rule_id")]
        public string RuleId { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("severity")]
        public string Severity { get; set; } = "review";

        [JsonPropertyName("condition")]
        public EOIRuleCondition Condition { get; set; }

        [JsonPropertyName("actions")]
        public EOIRuleAction[] Actions { get; set; }

        [JsonPropertyName("dwell_seconds")]
        public float? DwellSeconds { get; set; }

        [JsonPropertyName("reset_seconds")]
        public float? ResetSeconds { get; set; }
    }
}
