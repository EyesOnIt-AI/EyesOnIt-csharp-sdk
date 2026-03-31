using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK
{
    public class EOIMessage
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        public EOIMessage()
        {
        }

        public EOIMessage(bool success, string message = null) 
        { 
            Success = success;
            Message = message;
        }
    }
}
