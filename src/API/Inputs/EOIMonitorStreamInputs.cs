using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIMonitorStreamInputs {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        public EOIMonitorStreamInputs(string streamUrl, int? durationSeconds)
        {
            StreamUrl = streamUrl;
            DurationSeconds = durationSeconds;
        }

        //public string ToJson()
        //{
        //    var options = new JsonSerializerOptions
        //    {
        //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        //    };
        //    return JsonSerializer.Serialize(this, options);
        //}
    }
}
