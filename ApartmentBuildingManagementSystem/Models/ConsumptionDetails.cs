using Newtonsoft.Json;

namespace ApartmentBuildingManagementSystem.Models
{
    /// <summary>
    /// Class for the details of any kind of consumption (electricity, gas, water)
    /// </summary>
    public class ConsumptionDetails
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "consumption")]
        public double Consumption { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
    }
}
