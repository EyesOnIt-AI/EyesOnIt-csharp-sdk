using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIValidateLicenseInputs
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        public EOIValidateLicenseInputs(string key, string token)
        {
            Key = key;
            Token = token;
        }
    }
}
