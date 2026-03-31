using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIFacerecGroup
    {
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        public EOIFacerecGroup()
        {
        }

        public EOIFacerecGroup(string externalId, string displayName)
        {
            this.ExternalId = externalId;
            this.DisplayName = displayName;
        }
    }

    public class EOIFacerecImage
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        public EOIFacerecImage()
        {
        }

        public EOIFacerecImage(string path, string image)
        {
            this.Path = path;
            this.Image = image;
        }
    }

    public class EOIFacerecPersonDetailsResponse : EOIBaseOutputs
    {
        public string PersonId { get; set; }
        public string PersonName { get; set; }
        public List<EOIFacerecGroup> Groups { get; set; }
        public List<EOIFacerecImage> Images { get; set; }

        internal EOIFacerecPersonDetailsResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                if (dataElement.TryGetProperty("person_id", out JsonElement personIdElement))
                {
                    PersonId = JsonSerializer.Deserialize<string>(personIdElement.GetRawText());
                }

                if (dataElement.TryGetProperty("person_name", out JsonElement personNameElement))
                {
                    PersonName = JsonSerializer.Deserialize<string>(personNameElement.GetRawText());
                }

                if (dataElement.TryGetProperty("groups", out JsonElement groupsElement))
                {
                    Groups = JsonSerializer.Deserialize<List<EOIFacerecGroup>>(groupsElement.GetRawText());
                }

                if (dataElement.TryGetProperty("images", out JsonElement imagesElement))
                {
                    Images = JsonSerializer.Deserialize<List<EOIFacerecImage>>(imagesElement.GetRawText());
                }
            }
        }

        internal EOIFacerecPersonDetailsResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIFacerecPersonDetailsResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
