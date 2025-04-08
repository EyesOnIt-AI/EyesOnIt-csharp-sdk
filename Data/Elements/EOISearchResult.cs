using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
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

        [JsonPropertyName("stream")]
        public string Stream { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

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
