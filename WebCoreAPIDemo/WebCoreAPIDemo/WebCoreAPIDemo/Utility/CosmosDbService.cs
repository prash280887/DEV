using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAPIDemo.Models;

namespace WebCoreAPIDemo.Utility
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


        public async Task AddItemAsync(UserDetail item)
        {
            await this._container.CreateItemAsync<UserDetail>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<UserDetail>(id, new PartitionKey(id));
        }

        public async Task<UserDetail> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<UserDetail> response = await this._container.ReadItemAsync<UserDetail>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<UserDetail>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<UserDetail>(new QueryDefinition(queryString));
            List<UserDetail> results = new List<UserDetail>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, UserDetail item)
        {
            await this._container.UpsertItemAsync<UserDetail>(item, new PartitionKey(id));
        }
    }
}
