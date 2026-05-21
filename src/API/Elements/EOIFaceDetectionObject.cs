using System;
using System.IO;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIFaceDetectionObject
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
                        // clone into a Bitmap so it no longer depends on the MemoryStream
                        Image = new System.Drawing.Bitmap(tempImg);
                    }
                }
            }
        }

        private string base64Image;

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }

        public EOIFaceDetectionObject()
        {

        }
    }
}
