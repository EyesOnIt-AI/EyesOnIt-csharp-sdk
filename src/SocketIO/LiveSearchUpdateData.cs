// SocketClient.cs
using EyesOnItSDK.API.Elements;
using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class LiveSearchUpdateSimilarityImageData
    {
        [JsonPropertyName("seed_id")]
        public long? SeedId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("alert")]
        public bool? Alert { get; set; }

        [JsonPropertyName("threshold")]
        public int? Threshold { get; set; }
    }

    public class LiveSearchUpdateSimilarityData
    {
        [JsonPropertyName("images")]
        public LiveSearchUpdateSimilarityImageData[] Images { get; set; }
    }

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

        [JsonPropertyName("similarity")]
        public LiveSearchUpdateSimilarityData Similarity { get; set; }

        [JsonPropertyName("alert_threshold")]
        public int? AlertThreshold { get; set; }

        [JsonPropertyName("track_alert_cooldown_seconds")]
        public double? TrackAlertCooldownSeconds { get; set; }

        [JsonPropertyName("start_time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("end_time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeConverterUsingDateTimeParse))]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("notification")]
        public SocketNotificationData Notification { get; set; }
    }
}
