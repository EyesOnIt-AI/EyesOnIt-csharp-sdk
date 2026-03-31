// SocketClient.cs
using EyesOnItSDK.Data.Elements;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class LiveSearchUpdateSimilarityImageData
    {
        [JsonProperty("seed_id")]
        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; }

        [JsonProperty("image")]
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonProperty("alert")]
        [JsonPropertyName("alert")]
        public bool? Alert { get; set; }

        [JsonProperty("threshold")]
        [JsonPropertyName("threshold")]
        public double? Threshold { get; set; }
    }

    public class LiveSearchUpdateSimilarityData
    {
        [JsonProperty("images")]
        [JsonPropertyName("images")]
        public LiveSearchUpdateSimilarityImageData[] Images { get; set; }
    }

    public class LiveSearchUpdateData
    {
        [JsonProperty("search_id")]
        [JsonPropertyName("search_id")] 
        public int SearchId { get; set; }

        [JsonProperty("search_type")]
        [JsonPropertyName("search_type")]
        public string SearchType { get; set; }

        [JsonProperty("class_name")]
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonProperty("object_description")]
        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonProperty("face_match_type")]
        [JsonPropertyName("face_match_type")]
        public string FaceMatchType { get; set; }

        [JsonProperty("face_person_id")]
        [JsonPropertyName("face_person_id")]
        public string FacePersonId { get; set; }

        [JsonProperty("face_group_id")]
        [JsonPropertyName("face_group_id")]
        public string FaceGroupId { get; set; }

        [JsonProperty("similarity")]
        [JsonPropertyName("similarity")]
        public LiveSearchUpdateSimilarityData Similarity { get; set; }

        [JsonProperty("alert_threshold")]
        [JsonPropertyName("alert_threshold")]
        public double? AlertThreshold { get; set; }

        [JsonProperty("start_time")]
        [JsonPropertyName("start_time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        [JsonPropertyName("end_time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime EndTime { get; set; }

        [JsonProperty("active")]
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonProperty("notification")]
        [JsonPropertyName("notification")]
        public EOINotification Notification{ get; set; }
    }
}
