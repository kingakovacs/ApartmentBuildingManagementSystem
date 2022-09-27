using ApartmentBuildingManagementSystem.Models;

namespace ApartmentBuildingManagementSystem.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<ConsumptionDetails>> GetItemsAsync(string query);
        Task<ConsumptionDetails> GetItemAsync(string id);
        Task AddItemAsync(ConsumptionDetails consumptionDetail);
        Task UpdateItemAsync(string id, ConsumptionDetails consumptionDetail);
    }
}
