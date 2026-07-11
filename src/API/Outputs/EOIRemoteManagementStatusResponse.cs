using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIRemoteManagementStatus
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("service_running")]
        public bool ServiceRunning { get; set; }

        [JsonPropertyName("base_url_configured")]
        public bool BaseUrlConfigured { get; set; }

        [JsonPropertyName("api_key_file_configured")]
        public bool ApiKeyFileConfigured { get; set; }

        [JsonPropertyName("location_id_configured")]
        public bool LocationIdConfigured { get; set; }

        [JsonPropertyName("resource_types")]
        public List<string> ResourceTypes { get; set; } = new List<string>();

        [JsonPropertyName("sync_interval_seconds")]
        public int SyncIntervalSeconds { get; set; }

        [JsonPropertyName("settings_sync_enabled")]
        public bool SettingsSyncEnabled { get; set; }

        [JsonPropertyName("last_success_at")]
        public string LastSuccessAt { get; set; }

        [JsonPropertyName("last_failure_at")]
        public string LastFailureAt { get; set; }

        [JsonPropertyName("last_failure_message")]
        public string LastFailureMessage { get; set; }

        [JsonPropertyName("last_change_count")]
        public int LastChangeCount { get; set; }

        [JsonPropertyName("remote_record_counts")]
        public Dictionary<string, int> RemoteRecordCounts { get; set; } = new Dictionary<string, int>();

        [JsonPropertyName("cached_image_count")]
        public int CachedImageCount { get; set; }
    }

    public class EOIRemoteManagementStatusResponse : EOIBaseOutputs
    {
        public EOIRemoteManagementStatus Status { get; set; }

        internal EOIRemoteManagementStatusResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOIRemoteManagementStatusResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOIRemoteManagementStatusResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            Status = JsonSerializer.Deserialize<EOIRemoteManagementStatus>(dataElement.GetRawText());
        }
    }
}
