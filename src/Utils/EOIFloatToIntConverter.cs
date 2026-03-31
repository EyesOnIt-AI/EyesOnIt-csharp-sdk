using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class EOIFloatToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the numeric value as a double, then cast to int
        double doubleValue = reader.GetDouble();
        return (int)doubleValue;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        // Write the integer back as a JSON number
        writer.WriteNumberValue(value);
    }
}
