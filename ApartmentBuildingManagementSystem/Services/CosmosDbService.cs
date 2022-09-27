using ApartmentBuildingManagementSystem.Models;
using Microsoft.Azure.Cosmos;

namespace ApartmentBuildingManagementSystem.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(ConsumptionDetails consumptionDetail)
        {
            await this._container.CreateItemAsync<ConsumptionDetails>(consumptionDetail, new PartitionKey(consumptionDetail.Id.ToString()));
        }

        public async Task<ConsumptionDetails> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<ConsumptionDetails> response = await this._container.ReadItemAsync<ConsumptionDetails>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<ConsumptionDetails>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<ConsumptionDetails>(new QueryDefinition(queryString));
            List<ConsumptionDetails> results = new List<ConsumptionDetails>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, ConsumptionDetails consumptionDetail)
        {
            await this._container.UpsertItemAsync<ConsumptionDetails>(consumptionDetail, new PartitionKey(id));
        }
    }
}
