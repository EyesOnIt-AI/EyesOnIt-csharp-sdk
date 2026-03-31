using EyesOnItSDK.API.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOILiveSearchInputs : EOISearchInputs
    {

        [JsonPropertyName("duration_seconds")] 
        public int? DurationSeconds { get; set; }
        [JsonPropertyName("notification")]
        public EOINotification Notification { get; set; }

        public EOILiveSearchInputs() : base()
        {
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