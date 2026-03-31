using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API
{
    public class EOIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public EOIResponse(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

        public static EOIResponse Failure()
        {
            return new EOIResponse(false, "Unknown error");
        }

        public static EOIResponse CreateSuccess()
        {
            return new EOIResponse(true);
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

        internal static EOIResponse DefaultFailure()
        {
            return Failure();
        }

        internal static EOIResponse DefaultSuccess()
        {
            return CreateSuccess();
        }
    }
}
