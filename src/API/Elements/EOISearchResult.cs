using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOISearchResult
    {
        [JsonPropertyName("confidence")]
        public float Confidence { get; set; }

        [JsonPropertyName("source_type")]
        public string SourceType { get; set; }

        [JsonPropertyName("folder")]
        public string Folder { get; set; }

        [JsonPropertyName("file")]
        public string File { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("stream_url")]
        public string Stream { get; set; }

        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        [JsonPropertyName("stream_name")]
        public string StreamName { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("result_id")]
        public string ResultId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("facerec_person_id")]
        public string FacerecPersonId { get; set; }

        [JsonPropertyName("facerec_person_display_name")]
        public string FacerecPersonDisplayName { get; set; }

        public EOISearchResult()
        {
        }

        public static List<EOISearchResult> FromJson(string jsonString)
        {
            List<EOISearchResult> data = jsonString == null ? null : JsonSerializer.Deserialize<List<EOISearchResult>>(jsonString);

            return data;
        }
    }
}
