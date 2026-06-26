using EyesOnItSDK.API.Elements.VMS;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIDetectionConfig
    {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("class_threshold")]
        public int? ClassThreshold { get; set; }

        [JsonPropertyName("use_object_type_detection")]
        public bool UseObjectTypeDetection { get; set; } = true;

        [JsonPropertyName("use_natural_language")]
        public bool UseNaturalLanguage { get; set; } = false;

        [JsonPropertyName("use_face_recognition")]
        public bool UseFaceRecognition { get; set; } = false;

        [JsonPropertyName("use_similarity_detection")]
        public bool UseSimilarityDetection { get; set; } = false;

        [JsonPropertyName("object_size")]
        public int? ObjectSize { get; set; }

        [JsonPropertyName("object_descriptions")]
        public EOIObjectDescription[] ObjectDescriptions { get; set; }

        [JsonPropertyName("conditions")]
        public EOIDetectionCondition[] DetectionConditions{ get; set; }

        [JsonPropertyName("alert_seconds")]
        public float AlertSeconds { get; set; }

        [JsonPropertyName("reset_seconds")]
        public float ResetSeconds { get; set; }

        [JsonPropertyName("face_recognition")]
        public EOIFaceRecognitionConfig FaceRecognition { get; set; }

        [JsonPropertyName("similarity")]
        public EOISimilarityConfig Similarity { get; set; }


        // VMS Properties
        [JsonPropertyName("vms_config")]
        public EOIVMSDetectionConfig VMSConfig { get; set; }


        public EOIDetectionConfig()
        { 
        }
    }
}
