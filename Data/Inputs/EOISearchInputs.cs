using EyesOnItSDK.Data.Elements;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOISearchInputs {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")] 
        public string ObjectDescription { get; set; }

        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

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
            string objectDescription,
            string seedId,
            string image,
            string facePersonId,
            string faceGroupId,
            double? threshold,
            string[] streamList)
        {
            ClassName = className;
            ObjectDescription = objectDescription;
            SeedId = seedId;
            Image = image;
            FacePersonId = facePersonId;
            FaceGroupId = faceGroupId;
            AlertThreshold = threshold;
            StreamList = streamList;
        }
    }
}