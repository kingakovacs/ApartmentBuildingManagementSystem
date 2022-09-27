namespace ApartmentBuildingManagementSystem.Models
{
    public class Resident
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int ApartmentNumber { get; set; }

        public List<ConsumptionDetails>? ConsumptionDetails { get; set; }
    }
}
