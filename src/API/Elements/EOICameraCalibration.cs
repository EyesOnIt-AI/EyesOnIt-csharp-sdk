using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOICameraCalibration
    {
        [JsonPropertyName("calibration_id")]
        public string CalibrationId { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; } = "planar_homography";

        [JsonPropertyName("image_points")]
        public EOIVertex[] ImagePoints { get; set; } = new EOIVertex[0];

        [JsonPropertyName("world_points")]
        public EOIVertex[] WorldPoints { get; set; } = new EOIVertex[0];

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = "meters";

        [JsonPropertyName("rms_error")]
        public double? RmsError { get; set; }

        [JsonPropertyName("calibrated_at")]
        public string CalibratedAt { get; set; }
    }
}
