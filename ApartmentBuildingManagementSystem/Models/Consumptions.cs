namespace ApartmentBuildingManagementSystem.Models
{
    public class Consumptions
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public List<Resident>? Residents { get; set; }
    }
}
