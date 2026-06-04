using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIFaceDetectionObject : EOIBase64Image
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

        public EOIFaceDetectionObject()
        {

        }
    }
}
