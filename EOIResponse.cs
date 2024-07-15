using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK
{
    public class EOIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public object Data { get; set; }
        public Dictionary<string, int> ConfidenceLevels { get; set; }
        public string[] BoundingBoxObjects { get; set; }
        public string Image { get; set; }

        internal EOIResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }

        internal static EOIResponse DefaultFailure()
        {
            return new EOIResponse(false, "Unknown error");
        }

        internal static EOIResponse DefaultSuccess()
        {
            return new EOIResponse(true, null);
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonData = JsonSerializer.Serialize<EOIResponse>(this, options);

            return jsonData;
        }

        internal static EOIResponse FromJSON(string strResponse)
        {
            return new EOIResponse(true, null);
        }
    }
}
