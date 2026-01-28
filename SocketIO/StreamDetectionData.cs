// SocketClient.cs
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class StreamDetectionBoundsData
    {
        [JsonPropertyName("left")]
        public int Left { get; set; }

        [JsonPropertyName("top")]
        public int Top { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class StreamDetectionObjectData
    {
        [JsonPropertyName("object_descriptions")]
        public StreamObjectDescriptionsData[] ObjectDescriptions { get; set; }

        [JsonPropertyName("bounds")]
        public StreamDetectionBoundsData Bounds { get; set; }

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
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("count")]
        public int? Count{ get; set; }

        [JsonPropertyName("line_name")]
        public string LineName { get; set; }

        [JsonPropertyName("alert_direction")]
        public string AlertDirection { get; set; }

        [JsonPropertyName("objects")]
        public StreamDetectionObjectData[] Objects { get; set; }
    }

    public class StreamDetectionData
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        [JsonPropertyName("stream_name")]
        public string Name { get; set; }

        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("frame_num")]
        public int? FrameNum { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("condition")]
        public StreamDetectionConditionData Condition { get; set; }

        [JsonPropertyName("total_count")]
        public int? TotalCount { get; set; }

        [JsonPropertyName("result_id")]
        public string ResultId { get; set; }

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
        [JsonPropertyName("detections")] 
        public StreamDetectionData[] Detections { get; set; }

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
