using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIModelOptimizationCurrentModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public int? GpuId { get; set; }
        public string Stage { get; set; }
    }

    public class EOIModelOptimizationFailure
    {
        public string Message { get; set; }
        public string ModelId { get; set; }
        public string ModelLabel { get; set; }
        public int? GpuId { get; set; }
    }

    /// <summary>Typed response from the startup model optimization endpoints.</summary>
    public class EOIModelOptimizationStatusResponse : EOIBaseOutputs
    {
        public string State { get; set; }
        public int CompletedModels { get; set; }
        public int TotalModels { get; set; }
        public EOIModelOptimizationCurrentModel CurrentModel { get; set; }
        public EOIModelOptimizationFailure Failure { get; set; }
        public long? StartedUnix { get; set; }
        public long? UpdatedUnix { get; set; }
        public long? CompletedUnix { get; set; }

        internal EOIModelOptimizationStatusResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOIModelOptimizationStatusResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOIModelOptimizationStatusResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!(data is JsonElement value) || value.ValueKind != JsonValueKind.Object)
            {
                return;
            }

            State = GetString(value, "state");
            CompletedModels = GetInt(value, "completed_models") ?? 0;
            TotalModels = GetInt(value, "total_models") ?? 0;
            StartedUnix = GetLong(value, "started_unix");
            UpdatedUnix = GetLong(value, "updated_unix");
            CompletedUnix = GetLong(value, "completed_unix");

            if (value.TryGetProperty("current_model", out JsonElement current) && current.ValueKind == JsonValueKind.Object)
            {
                CurrentModel = new EOIModelOptimizationCurrentModel
                {
                    Id = GetString(current, "id"),
                    Label = GetString(current, "label"),
                    GpuId = GetInt(current, "gpu_id"),
                    Stage = GetString(current, "stage"),
                };
            }

            if (value.TryGetProperty("failure", out JsonElement failure) && failure.ValueKind == JsonValueKind.Object)
            {
                Failure = new EOIModelOptimizationFailure
                {
                    Message = GetString(failure, "message"),
                    ModelId = GetString(failure, "model_id"),
                    ModelLabel = GetString(failure, "model_label"),
                    GpuId = GetInt(failure, "gpu_id"),
                };
            }
        }

        private static string GetString(JsonElement value, string property)
        {
            return value.TryGetProperty(property, out JsonElement element) && element.ValueKind == JsonValueKind.String
                ? element.GetString()
                : null;
        }

        private static int? GetInt(JsonElement value, string property)
        {
            return value.TryGetProperty(property, out JsonElement element) && element.TryGetInt32(out int parsed)
                ? parsed
                : (int?)null;
        }

        private static long? GetLong(JsonElement value, string property)
        {
            return value.TryGetProperty(property, out JsonElement element) && element.TryGetInt64(out long parsed)
                ? parsed
                : (long?)null;
        }
    }
}
