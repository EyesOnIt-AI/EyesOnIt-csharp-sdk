using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOILastDetection
    {
        [JsonPropertyName("prompt_values")]
        public Dictionary<string, int> DescriptionValues { get; set; }

        [JsonPropertyName("alerting_prompt")]
        public string AlertingDescription { get; set; }

        [JsonPropertyName("alert_time")]
        public DateTime? AlertTime { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        public EOILastDetection()
        {
            DescriptionValues = new Dictionary<string, int>();
            AlertingDescription = string.Empty;
            AlertTime = null;
        }

        public static EOILastDetection FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new DateTimeConverterUsingDateTimeParse()
                }
            };

            EOILastDetection data = jsonString == null ? null : JsonSerializer.Deserialize<EOILastDetection>(jsonString, options);

            return data;
        }

    }
}
