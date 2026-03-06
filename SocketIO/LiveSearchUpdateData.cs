// SocketClient.cs
using EyesOnItSDK.Data.Elements;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class LiveSearchUpdateData
    {
        [JsonPropertyName("search_id")] 
        public int SearchId { get; set; }

        [JsonPropertyName("search_type")]
        public string SearchType { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("face_match_type")]
        public string FaceMatchType { get; set; }

        [JsonPropertyName("face_person_id")]
        public string FacePersonId { get; set; }

        [JsonPropertyName("face_group_id")]
        public string FaceGroupId { get; set; }

        [JsonPropertyName("seed_id")]
        public string SeedId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("alert_threshold")]
        public double? AlertThreshold { get; set; }

        [JsonPropertyName("start_time")]
        [JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("end_time")]
        [JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("notification")]
        public EOINotification Notification{ get; set; }
    }
}
