using ApartmentBuildingManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentBuildingManagementSystem.Data
{
    public class CosmosDbContext: DbContext
    {
        public DbSet<Consumptions>? Consumptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "https://bms-db.documents.azure.com:443/",
                "H2ETIIy1OprdrFnWPMGK3RQ9Wx9t8jpCrEDib4EsnJrg281JowMw7ZhcUigmley7NbwjlW6p5smbWOHBQB0WRg==",
                "BlockManagementSystem");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuring Consumptions
            modelBuilder.Entity<Consumptions>()
                    .ToContainer("Consumptions") // ToContainer
                    .HasPartitionKey(e => e.Id); // Partition Key

            modelBuilder.Entity<Consumptions>().OwnsMany(p => p.Residents);
            modelBuilder.Entity<Resident>().OwnsMany(p => p.ConsumptionDetails);
        }
    }
}
