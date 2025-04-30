using EyesOnItSDK.Data.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOILiveSearchInputs {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("alert_threshold")]
        public int? AlertThreshold { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        [JsonPropertyName("notification")]
        public EOINotification Notification { get; set; }

        public EOILiveSearchInputs(string className, string objectDescription, int alertThreshold, EOINotification notification, int? durationSeconds)
        {
            ClassName = className;
            ObjectDescription = objectDescription;
            AlertThreshold = alertThreshold;
            DurationSeconds = durationSeconds;
            Notification = notification;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonData = JsonSerializer.Serialize(this, options);

            return jsonData;
        }

        public static EOILiveSearchInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOILiveSearchInputs liveSearchInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOILiveSearchInputs>(jsonString, options);

            return liveSearchInputs;
        }
    }
}
