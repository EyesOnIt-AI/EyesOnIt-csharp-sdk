using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK
{
    public class EOIMessage
    {
        [JsonPropertyName("message")]
        [JsonConverter(typeof(MessageConverter))]
        public string Message { get; set; }

        [JsonPropertyName("detail")]
        [JsonConverter(typeof(MessageConverter))]
        public string Detail { get; set; }

        [JsonPropertyName("image")]
        [JsonConverter(typeof(MessageConverter))]
        public string Image { get; set; }

        [JsonPropertyName("bounding_box_objects")]
        [JsonConverter(typeof(MessageConverter))]
        public string[] BoundingBoxObjects { get; set; }

        public string RawResponse { get; set; }

        [JsonIgnore] // Prevent this from being serialized
        public Dictionary<string, int> JSON { get; set; }

        public static EOIMessage FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new MessageConverter() }
            };

            EOIMessage message = null;

            if (jsonString != null)
            {
                message = JsonSerializer.Deserialize<EOIMessage>(jsonString, options);

                if (message != null)
                {
                    message.RawResponse = jsonString;
                }
            }

            return message;
        }
    }

    public class MessageConverter : JsonConverter<EOIMessage>
    {
        public override EOIMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var eoiMessage = new EOIMessage();

            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                if (doc.RootElement.TryGetProperty("message", out JsonElement messageElement))
                {
                    if (messageElement.ValueKind == JsonValueKind.String)
                    {
                        eoiMessage.Message = messageElement.GetString();
                    }
                    else if (messageElement.ValueKind == JsonValueKind.Object)
                    {
                        eoiMessage.JSON = JsonSerializer.Deserialize<Dictionary<string, int>>(messageElement.GetRawText(), options);
                    }
                }
                else if (doc.RootElement.TryGetProperty("detail", out JsonElement detailElement))
                {
                    if (detailElement.ValueKind == JsonValueKind.String)
                    {
                        eoiMessage.Detail = detailElement.GetString();
                    }
                }
                else if (doc.RootElement.TryGetProperty("image", out JsonElement imageElement))
                {
                    if (imageElement.ValueKind == JsonValueKind.String)
                    {
                        eoiMessage.Image = imageElement.GetString();
                    }
                }
                else if (doc.RootElement.TryGetProperty("bounding_box_objects", out JsonElement boundingBoxObjectsElement))
                {
                    if (boundingBoxObjectsElement.ValueKind == JsonValueKind.Array)
                    {
                        var boundingBoxObjects = new List<string>();
                        foreach (var item in boundingBoxObjectsElement.EnumerateArray())
                        {
                            if (item.ValueKind == JsonValueKind.String)
                            {
                                boundingBoxObjects.Add(item.GetString());
                            }
                        }
                        eoiMessage.BoundingBoxObjects = boundingBoxObjects.ToArray();
                    }
                }
            }

            return eoiMessage;
        }

        public override void Write(Utf8JsonWriter writer, EOIMessage value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value.Message != null)
            {
                writer.WriteString("message", value.Message);
            }
            else if (value.JSON != null)
            {
                writer.WritePropertyName("message");
                JsonSerializer.Serialize(writer, value.JSON, options);
            }

            writer.WriteEndObject();
        }
    }
}
