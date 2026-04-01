using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIObjectDescription
    {
        [JsonPropertyName("display_text")]
        public string DisplayText { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
        
        [JsonPropertyName("threshold")]
        public int? Threshold { get; set; }

        [JsonPropertyName("alert")]
        public bool? Alert { get; set; }

        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }

        [JsonPropertyName("over_threshold")]
        public bool? OverThreshold { get; set; }

        [JsonPropertyName("background_prompt")]
        public bool BackgroundPrompt { get; set; }

        public EOIObjectDescription()
        {
            this.Text = null;
            this.Threshold = null;
            this.Confidence = null;
            this.OverThreshold = null;
            this.BackgroundPrompt = false;
        }

        public EOIObjectDescription(string text, bool backgroundPrompt) 
        {
            Text = text;
            BackgroundPrompt = backgroundPrompt;
            Threshold = null;
        }
    }
}
