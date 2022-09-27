using Newtonsoft.Json;

namespace ApartmentBuildingManagementSystem.Models
{
    public enum ConsumptionType {
        HotWater,
        ColdWater,
        Electricity,
        Gas
    };

    /// <summary>
    /// Class for the details of any kind of consumption (electricity, gas, water)
    /// </summary>
    public class ConsumptionDetails
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public ConsumptionType ConsumptionType { get; set; }

        public double Consumption { get; set; }

        public double Price { get; set; }
    }
}
