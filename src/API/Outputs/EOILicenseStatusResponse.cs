using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOILicenseStatusResponse : EOIBaseOutputs
    {
        public string LicenseKey { get; set; }
        public bool? Valid { get; set; }
        public string LicenseType { get; set; }
        public JsonElement Permissions { get; set; }
        public bool HasPermissions { get; set; }

        internal EOILicenseStatusResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOILicenseStatusResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOILicenseStatusResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            if (dataElement.TryGetProperty("license_key", out JsonElement licenseKeyElement) &&
                licenseKeyElement.ValueKind == JsonValueKind.String)
            {
                LicenseKey = licenseKeyElement.GetString();
            }

            if (dataElement.TryGetProperty("valid", out JsonElement validElement) &&
                (validElement.ValueKind == JsonValueKind.True || validElement.ValueKind == JsonValueKind.False))
            {
                Valid = validElement.GetBoolean();
            }

            if (dataElement.TryGetProperty("license_type", out JsonElement licenseTypeElement) &&
                licenseTypeElement.ValueKind == JsonValueKind.String)
            {
                LicenseType = licenseTypeElement.GetString();
            }

            if (dataElement.TryGetProperty("permissions", out JsonElement permissionsElement))
            {
                Permissions = permissionsElement.Clone();
                HasPermissions = true;
            }
        }
    }
}
