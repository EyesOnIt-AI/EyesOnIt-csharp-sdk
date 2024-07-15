using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIBoundingBox
    {
        [JsonPropertyName("bounding_box_enabled")]
        public bool BoundingBoxEnabled{ get; set; }
        
        [JsonPropertyName("detect_people")]
        public bool DetectPeople{ get; set; }

        [JsonPropertyName("detect_vehicles")]
        public bool DetectVehicles { get; set; }

        [JsonPropertyName("detect_bags")]
        public bool DetectBags { get; set; }

        [JsonPropertyName("person_confidence_threshold")]
        public int PeopleConfidenceThreshold { get; set; }

        [JsonPropertyName("vehicle_confidence_threshold")]
        public int VehiclesConfidenceThreshold { get; set; }

        [JsonPropertyName("bag_confidence_threshold")]
        public int BagsConfidenceThreshold { get; set; }

        public EOIBoundingBox()
        {
            this.BoundingBoxEnabled = false;
            this.DetectPeople = false;
            this.DetectVehicles = false;
            this.DetectBags = false;
            this.PeopleConfidenceThreshold = 30;
            this.VehiclesConfidenceThreshold = 30;
            this.BagsConfidenceThreshold = 30;
        }

        public static EOIBoundingBox NoBoundingBox()
        {
            return new EOIBoundingBox() { 
                BoundingBoxEnabled = false, 
                DetectPeople = false, 
                DetectVehicles = false, 
                DetectBags = false, 
                PeopleConfidenceThreshold = 30,
                VehiclesConfidenceThreshold = 30,
                BagsConfidenceThreshold = 30
            };
        }
    }
}
