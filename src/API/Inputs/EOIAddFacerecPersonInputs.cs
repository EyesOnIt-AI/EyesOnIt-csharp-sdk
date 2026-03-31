using EyesOnItSDK.Data.Elements;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIAddFacerecPersonImage
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("file_path")]
        public string FilePath { get; set; }

        [JsonPropertyName("capture_time")]
        public string CaptureTime { get; set; }


        public EOIAddFacerecPersonImage(string image, string filePath, string captureTime)
        {
            this.Image = image;
            this.FilePath = filePath;
            this.CaptureTime = captureTime;
        }
    }

    public class EOIAddFacerecPersonInputs : EOIBaseInputs
    {
        [JsonPropertyName("person_images")]
        public List<EOIAddFacerecPersonImage> PersonImages { get; set; } = new List<EOIAddFacerecPersonImage>();

        [JsonPropertyName("person_id")]
        public string PersonId { get; set; }

        [JsonPropertyName("person_display_name")]
        public string PersonDisplayName { get; set; }

        [JsonPropertyName("person_groups")]
        public string[] PersonGroups { get; set; }

        public EOIAddFacerecPersonInputs()
        {
        }

        public EOIAddFacerecPersonInputs(string personId, string personDisplayName, string[] personGroups)
        {
            this.PersonId = personId;
            this.PersonDisplayName = personDisplayName;
            this.PersonGroups = personGroups;
        }

        public void AddImageBase64(string image, string filePath)
        {
            this.PersonImages.Add(new EOIAddFacerecPersonImage(image, filePath, DateTime.UtcNow.ToString("o")));
        }
    }
}
