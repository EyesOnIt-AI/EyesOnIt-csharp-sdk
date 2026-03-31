using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOILastDetectionInfo
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("prompt_values")]
        public Dictionary<string, double> DescriptionValues { get; set; }

        [JsonPropertyName("alerting_prompt")]
        public string AlertingDescription { get; set; }

        [JsonPropertyName("alert_time")]
        public string AlertTime { get; set; }
    }
}
