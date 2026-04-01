using EyesOnItSDK.API.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIAddFacerecPeopleInputs
    {
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; }


        public EOIAddFacerecPeopleInputs(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
