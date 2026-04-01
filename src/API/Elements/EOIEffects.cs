using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIEffects
    {
        [JsonPropertyName("show_motion")]
        public bool ShowMotion { get; set; }

        [JsonPropertyName("show_bounding_boxes")]
        public bool ShowBoundingBoxes { get; set; }

        [JsonPropertyName("show_bounding_box_labels")] 
        public bool ShowBoundingBoxLabels { get; set; }

        [JsonPropertyName("show_lines")]
        public bool ShowLines { get; set; }

        [JsonPropertyName("show_regions")]
        public bool ShowRegions { get; set; }

        [JsonPropertyName("show_preliminary_detections")]
        public bool ShowPreliminaryDetections { get; set; }

        [JsonPropertyName("show_validated_detections")]
        public bool ShowValidatedDetections { get; set; }

        [JsonPropertyName("blur_non_validated_detections")]
        public bool BlurNonValidatedDetections{ get; set; }

        [JsonPropertyName("show_confidence_levels")]
        public bool ShowConfidenceLevels { get; set; }

        [JsonPropertyName("show_object_count")]
        public bool ShowObjectCount{ get; set; }

        [JsonPropertyName("show_frame_number")]
        public bool ShowFrameNumber{ get; set; }

        [JsonPropertyName("show_track_id")]
        public bool ShowTrackId { get; set; }

        [JsonPropertyName("show_alert_text")]
        public bool ShowAlertText { get; set; }

        [JsonPropertyName("font_scale")]
        public int FontScale { get; set; }

    }
}
