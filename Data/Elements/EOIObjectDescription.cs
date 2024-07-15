using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIObjectDescription
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        
        [JsonPropertyName("threshold")]
        public int? Threshold { get; set; }

        [JsonPropertyName("confidence")]
        public float? Confidence { get; set; }

        [JsonPropertyName("over_threshold")]
        public bool? OverThreshold { get; set; }

        [JsonPropertyName("background_prompt")]
        public bool BackgroundDescription { get; set; }

        public EOIObjectDescription()
        {
            this.Text = null;
            this.Threshold = null;
            this.Confidence = null;
            this.OverThreshold = null;
            this.BackgroundDescription = false;
        }

        public EOIObjectDescription(string text, bool backgroundDescription) 
        {
            this.Text = text;
            this.BackgroundDescription = backgroundDescription;
            this.Threshold = null;
        }
    }
}
