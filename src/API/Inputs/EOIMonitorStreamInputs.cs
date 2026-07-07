using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIMonitorStreamInputs {
        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        public EOIMonitorStreamInputs(string streamId, int? durationSeconds)
        {
            StreamId = streamId;
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
