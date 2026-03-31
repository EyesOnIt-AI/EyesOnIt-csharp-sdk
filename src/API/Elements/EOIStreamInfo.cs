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
