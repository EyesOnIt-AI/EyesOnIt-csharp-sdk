using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOILicenseValidityResponse : EOIBaseOutputs
    {
        public bool? Entered { get; set; }
        public bool? Valid { get; set; }

        internal EOILicenseValidityResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOILicenseValidityResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOILicenseValidityResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            if (dataElement.TryGetProperty("entered", out JsonElement enteredElement) &&
                (enteredElement.ValueKind == JsonValueKind.True || enteredElement.ValueKind == JsonValueKind.False))
            {
                Entered = enteredElement.GetBoolean();
            }

            if (dataElement.TryGetProperty("valid", out JsonElement validElement) &&
                (validElement.ValueKind == JsonValueKind.True || validElement.ValueKind == JsonValueKind.False))
            {
                Valid = validElement.GetBoolean();
            }
        }
    }
}
