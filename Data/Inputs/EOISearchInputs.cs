using EyesOnItSDK.Data.Elements;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOISearchInputs {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("search_type")]
        public string SearchType { get; set; }

        [JsonPropertyName("object_description")] 
        public string ObjectDescription { get; set; }

        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
        
        // Face recognition match type: `person` or `group`
        [JsonPropertyName("face_match_type")]
        public string FaceMatchType { get; set; }

        [JsonPropertyName("face_person_id")]
        public string FacePersonId { get; set; }

        [JsonPropertyName("face_group_id")]
        public string FaceGroupId { get; set; }

        [JsonPropertyName("alert_threshold")]
        public double? AlertThreshold { get; set; }

        [JsonPropertyName("stream_list")]
        public string[] StreamList { get; set; }

        public EOISearchInputs()
        {
        }

        public EOISearchInputs(string className,
            string searchType,
            string objectDescription,
            string seedId,
            string image,
            string faceMatchType,
            string facePersonId,
            string faceGroupId,
            double? threshold,
            string[] streamList)
        {
            ClassName = className;
            SearchType = searchType;
            ObjectDescription = objectDescription;
            SeedId = seedId;
            Image = image;
            FaceMatchType = faceMatchType;
            FacePersonId = facePersonId;
            FaceGroupId = faceGroupId;
            AlertThreshold = threshold;
            StreamList = streamList;
        }
    }
}