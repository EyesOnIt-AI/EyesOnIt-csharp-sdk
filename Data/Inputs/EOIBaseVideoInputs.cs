using EyesOnItSDK.Data.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIBaseVideoInputs : EOIBaseInputs
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("frame_rate")]
        public int? FrameRate { get; set; }

        [JsonPropertyName("lines")]
        public EOILine[] Lines { get; set; }

        [JsonPropertyName("index_for_search")]
        public bool IndexForSearch { get; set; }

        [JsonPropertyName("search_index_types")]
        public string[] SearchIndexTypes { get; set; }

        [JsonPropertyName("effects")]
        public EOIEffects Effects { get; set; }

        [JsonPropertyName("recording")]
        public EOIRecording Recording { get; set; }
    }
}
