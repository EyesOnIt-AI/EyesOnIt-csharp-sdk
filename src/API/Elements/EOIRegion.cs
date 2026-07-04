using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIRegion
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("polygon")]
        public EOIVertex[] Polygon { get; set; }

        [JsonPropertyName("motion_detection")]
        public EOIMotionDetection MotionDetection { get; set; }

        [JsonPropertyName("detection_configs")]
        public EOIDetectionConfig[] DetectionConfigs { get; set; }

        [JsonPropertyName("rules")]
        public EOIRule[] Rules { get; set; }

        [JsonPropertyName("interaction_rules")]
        public EOIInteractionRule[] InteractionRules { get; set; }

        public EOIRegion()
        {
            Enabled = true;
        }
    }

    public class EOIInteractionRule
    {
        [JsonPropertyName("rule_id")]
        public string RuleId { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("mode")]
        public string Mode { get; set; } = "review_only";

        [JsonPropertyName("primary_config_id")]
        public string PrimaryConfigId { get; set; }

        [JsonPropertyName("secondary_config_id")]
        public string SecondaryConfigId { get; set; }

        [JsonPropertyName("dwell_seconds")]
        public float? DwellSeconds { get; set; }

        [JsonPropertyName("reset_seconds")]
        public float? ResetSeconds { get; set; }

        [JsonPropertyName("severity")]
        public string Severity { get; set; }

        [JsonPropertyName("parameters")]
        public System.Collections.Generic.Dictionary<string, object> Parameters { get; set; }
    }
}
