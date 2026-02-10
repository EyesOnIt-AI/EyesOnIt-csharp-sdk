using EyesOnItSDK.Data.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
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
