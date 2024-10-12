using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIAddStreamInputs : EOIBaseInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("frame_rate")]
        public int? FrameRate { get; set; }

        [JsonPropertyName("lines")]
        public EOILine[] Lines { get; set; }

        [JsonPropertyName("notification")]
        public EOINotification Notification { get; set; }

        [JsonPropertyName("recording")]
        public EOIRecording Recording { get; set; }

        [JsonPropertyName("effects")]
        public EOIEffects Effects { get; set; }
        

        public EOIAddStreamInputs() : base()
        {
            this.FrameRate = 5;
        }

        public EOIAddStreamInputs(string streamUrl, 
            string name, 
            int frameRate, 
            EOIRegion[] regions,
            EOILine[] lines,
            EOINotification notification, 
            EOIRecording recording, 
            EOIEffects effects) : base()
        {
            this.StreamUrl = streamUrl;
            this.Name = name;
            this.FrameRate = frameRate;
            this.Regions = regions;
            this.Lines = lines;
            this.Notification = notification;
            this.Recording = recording;
            this.Effects = effects;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonData = JsonSerializer.Serialize<EOIAddStreamInputs>(this, options);

            return jsonData;
        }

        public static EOIAddStreamInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOIAddStreamInputs addStreamInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOIAddStreamInputs>(jsonString, options);

            return addStreamInputs;
        }
    }
}
