// SocketClient.cs
using System;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamDetectionBoundsData
    {
        [JsonProperty("left")]
        [JsonPropertyName("left")]
        public int Left { get; set; }

        [JsonProperty("top")]
        [JsonPropertyName("top")]
        public int Top { get; set; }

        [JsonProperty("width")]
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class StreamDetectionObjectData
    {
        [JsonProperty("object_descriptions")]
        [JsonPropertyName("object_descriptions")]
        public StreamObjectDescriptionsData[] ObjectDescriptions { get; set; }

        [JsonProperty("bounds")]
        [JsonPropertyName("bounds")]
        public StreamDetectionBoundsData Bounds { get; set; }

        [JsonProperty("image")]
        [JsonPropertyName("image")]
        public string Base64Image
        {
            get
            {
                return base64Image;
            }
            set
            {
                base64Image = value;
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (var ms = new MemoryStream(imageBytes))
                    using (var tempImg = System.Drawing.Image.FromStream(ms))
                    {
                        // clone into a Bitmap so it no longer depends on the MemoryStream
                        Image = new System.Drawing.Bitmap(tempImg);
                    }
                }
            }
        }

        private string base64Image;

        public System.Drawing.Image Image { get; set; }
    }

    public class StreamDetectionConditionData
    {
        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonProperty("count")]
        [JsonPropertyName("count")]
        public int? Count{ get; set; }

        [JsonProperty("line_name")]
        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonProperty("alert_direction")]
        [JsonPropertyName("alert_direction")]
        public string AlertDirection { get; set; }

        [JsonProperty("objects")]
        [JsonPropertyName("objects")]
        public StreamDetectionObjectData[] Objects { get; set; }
    }

    public class StreamDetectionData
    {
        [JsonProperty("stream_url")]
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonProperty("stream_name")]
        [JsonPropertyName("stream_name")]
        public string Name { get; set; }

        [JsonProperty("event")]
        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonProperty("region")]
        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonProperty("time")]
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonProperty("frame_num")]
        [JsonPropertyName("frame_num")]
        public int? FrameNum { get; set; }

        [JsonProperty("class_name")]
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonProperty("object_description")]
        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonProperty("condition")]
        [JsonPropertyName("condition")]
        public StreamDetectionConditionData Condition { get; set; }

        [JsonProperty("total_count")]
        [JsonPropertyName("total_count")]
        public int? TotalCount { get; set; }

        [JsonProperty("result_id")]
        [JsonPropertyName("result_id")]
        public string ResultId { get; set; }

        [JsonProperty("image")]
        [JsonPropertyName("image")]
        public string Base64Image
        {
            get
            {
                return base64Image;
            }
            set
            {
                base64Image = value;
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (var ms = new MemoryStream(imageBytes))
                    using (var tempImg = System.Drawing.Image.FromStream(ms))
                    {
                        // clone into a Bitmap so it no longer depends on the MemoryStream
                        Image = new System.Drawing.Bitmap(tempImg);
                    }
                }
            }
        }

        private string base64Image;

        public System.Drawing.Image Image { get; set; }
    }

    public class StreamDetectionsData
    {
        [JsonProperty("detections")]
        [JsonPropertyName("detections")] 
        public StreamDetectionData[] Detections { get; set; }

        [JsonProperty("image")]
        [JsonPropertyName("image")]
        public string Base64Image
        {
            get
            {
                return base64Image;
            }
            set
            {
                base64Image = value;
                if (!string.IsNullOrEmpty(value))
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (var ms = new MemoryStream(imageBytes))
                    using (var tempImg = System.Drawing.Image.FromStream(ms))
                    {
                        // clone into a Bitmap so it no longer depends on the MemoryStream
                        Image = new System.Drawing.Bitmap(tempImg);
                    }
                }
            }
        }

        private string base64Image;

        public System.Drawing.Image Image { get; set; }
    }
}
