using EyesOnItSDK.API.Elements;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOISearchInputs {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("search_type")]
        public string SearchType { get; set; }

        [JsonPropertyName("object_description")] 
        public string ObjectDescription { get; set; }

        [JsonPropertyName("similarity")]
        public EOISimilarityConfig Similarity { get; set; }
        
        // Face recognition match type: `person` or `group`
        [JsonPropertyName("face_match_type")]
        public string FaceMatchType { get; set; }

        [JsonPropertyName("face_person_id")]
        public string FacePersonId { get; set; }

        [JsonPropertyName("face_group_id")]
        public string FaceGroupId { get; set; }

        [JsonPropertyName("alert_threshold")]
        public int? AlertThreshold { get; set; }

        [JsonPropertyName("stream_ids")]
        public string[] StreamIds { get; set; }

        public EOISearchInputs()
        {
        }

        public EOISearchInputs(string className,
            string searchType,
            string objectDescription,
            EOISimilarityConfig similarity,
            string faceMatchType,
            string facePersonId,
            string faceGroupId,
            int? threshold,
            string[] streamIds)
        {
            ClassName = className;
            SearchType = searchType;
            ObjectDescription = objectDescription;
            Similarity = similarity;
            FaceMatchType = faceMatchType;
            FacePersonId = facePersonId;
            FaceGroupId = faceGroupId;
            AlertThreshold = threshold;
            StreamIds = streamIds;
        }
    }
}
