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

        [JsonPropertyName("object_size")]
        public int ObjectSize { get; set; }

        [JsonPropertyName("alerting")]
        public EOIAlerting Alerting { get; set; }

        [JsonPropertyName("efficient_detection")]
        public EOIMotionDetection MotionDetection { get; set; }

        [JsonPropertyName("bounding_box")]
        public EOIBoundingBox BoundingBox{ get; set; }

        [JsonPropertyName("frame_rate")]
        public int? FrameRate { get; set; }


        public EOIAddStreamInputs() : base()
        {
            this.Alerting = new EOIAlerting();
            this.MotionDetection = EOIMotionDetection.NoMotionDetection();
            this.BoundingBox= new EOIBoundingBox();
            this.StreamUrl = "";
        }

        public EOIAddStreamInputs(
            string streamUrl,
            string name,
            EOIRegion[] regions,
            int objectSize,
            EOIObjectDescription[] objectDescriptions,
            EOIAlerting alerting,
            EOIMotionDetection motionDetection = null,
            EOIBoundingBox boundingBox = null,
            int? frameRate = 5) : base(objectDescriptions, regions)
        {
            this.StreamUrl = streamUrl;
            this.Name = name;
            this.ObjectSize = objectSize;
            this.Alerting = alerting;
            this.MotionDetection = motionDetection ?? EOIMotionDetection.NoMotionDetection();
            this.BoundingBox = boundingBox ?? EOIBoundingBox.NoBoundingBox();
            this.FrameRate = frameRate ?? 5;
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
