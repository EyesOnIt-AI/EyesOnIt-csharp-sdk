using EyesOnItSDK.API.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIAddStreamInputs : EOIBaseVideoInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("notification")]
        public EOINotification Notification { get; set; }


        public EOIAddStreamInputs() : base()
        {
            this.FrameRate = 5;
            this.IndexForSearch = false;
        }

        public EOIAddStreamInputs(string streamUrl, 
            string name, 
            int frameRate, 
            EOIRegion[] regions,
            EOILine[] lines,
            EOINotification notification, 
            EOIRecording recording, 
            EOIEffects effects,
            bool indexForSearch,
            string[] searchIndexTypes) : base()
        {
            this.StreamUrl = streamUrl;
            this.Name = name;
            this.FrameRate = frameRate;
            this.Regions = regions;
            this.Lines = lines;
            this.Notification = notification;
            this.Recording = recording;
            this.Effects = effects;
            this.IndexForSearch = indexForSearch;
            this.SearchIndexTypes = searchIndexTypes;
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
