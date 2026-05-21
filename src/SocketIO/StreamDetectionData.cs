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
        [JsonPropertyName("detection_type")]
        public string DetectionType { get; set; }

        [JsonPropertyName("detection_types")]
        public string[] DetectionTypes { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("class_confidence")]
        public float? ClassConfidence { get; set; }

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

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }

        [JsonPropertyName("face")]
        public StreamDetectionFaceData Face { get; set; }

        [JsonPropertyName("similarity")]
        public StreamDetectionSimilarityData Similarity { get; set; }
    }

    public class StreamDetectionFaceData
    {
        [JsonPropertyName("person_external_id")]
        public string PersonExternalId { get; set; }

        [JsonPropertyName("person_display_name")]
        public string PersonDisplayName { get; set; }

        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }

        [JsonPropertyName("face_quality")]
        public float? FaceQuality { get; set; }

        [JsonPropertyName("group_external_id")]
        public string GroupExternalId { get; set; }

        [JsonPropertyName("group_display_name")]
        public string GroupDisplayName { get; set; }

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
                        Image = new System.Drawing.Bitmap(tempImg);
                    }
                }
            }
        }

        private string base64Image;

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }
    }

    public class StreamDetectionSimilarityData
    {
        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }
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
        public string StreamName { get; set; }

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

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }

        [JsonPropertyName("alert_stream_id")]
        public string AlertStreamId { get; set; }

        [JsonPropertyName("alert_id")]
        public string AlertId { get; set; }

        [JsonPropertyName("alert_rtsp_url")]
        public string AlertRtspUrl { get; set; }
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

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }
    }
}
