using EyesOnItSDK.Data.Elements;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIStreamInfo
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("last_alert_time")]
        public DateTime LastAlertTime { get; set; }

        [JsonPropertyName("frame_rate")]
        public int FrameRate { get; set; }

        [JsonPropertyName("regions")]
        public EOIRegion[] Regions { get; set; }

        [JsonPropertyName("notification")]
        public EOINotification Notification { get; set; }


        [JsonPropertyName("gpu_util")]
        public List<int> GpuUtil { get; set; }

        public bool IsMonitoring
        {
            get
            {
                return Status == "MONITORING" || Status == "ALERTING";
            }
        }

        public bool IsAlerting
        {
            get
            {
                return Status == "ALERTING";
            }
        }

        public static List<EOIStreamInfo> FromJson(string jsonString)
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

            List<EOIStreamInfo> data = jsonString == null ? null : JsonSerializer.Deserialize<List<EOIStreamInfo>>(jsonString, options);

            return data;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonData = JsonSerializer.Serialize<EOIStreamInfo>(this, options);

            return jsonData;
        }
    }
}
