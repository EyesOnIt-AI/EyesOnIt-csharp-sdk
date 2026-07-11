using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    /// <summary>Startup model readiness payload sent by the EyesOnIt Socket.IO server.</summary>
    public class ModelOptimizationStatusData
    {
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("completed_models")]
        public int CompletedModels { get; set; }

        [JsonPropertyName("total_models")]
        public int TotalModels { get; set; }

        [JsonPropertyName("current_model")]
        public ModelOptimizationCurrentModelData CurrentModel { get; set; }

        [JsonPropertyName("failure")]
        public ModelOptimizationFailureData Failure { get; set; }

        [JsonPropertyName("started_unix")]
        public long? StartedUnix { get; set; }

        [JsonPropertyName("updated_unix")]
        public long? UpdatedUnix { get; set; }

        [JsonPropertyName("completed_unix")]
        public long? CompletedUnix { get; set; }
    }

    public class ModelOptimizationCurrentModelData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("gpu_id")]
        public int? GpuId { get; set; }

        [JsonPropertyName("stage")]
        public string Stage { get; set; }
    }

    public class ModelOptimizationFailureData
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("model_id")]
        public string ModelId { get; set; }

        [JsonPropertyName("model_label")]
        public string ModelLabel { get; set; }

        [JsonPropertyName("gpu_id")]
        public int? GpuId { get; set; }
    }
}
