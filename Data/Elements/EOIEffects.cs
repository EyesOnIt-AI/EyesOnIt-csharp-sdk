using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIEffects
    {
        [JsonPropertyName("show_motion")]
        public bool ShowMotion { get; set; }

        [JsonPropertyName("show_bounding_boxes")]
        public bool ShowBoundingBoxes { get; set; }

        [JsonPropertyName("show_lines")]
        public bool ShowLines { get; set; }

        [JsonPropertyName("show_regions")]
        public bool ShowRegions { get; set; }

    }
}
