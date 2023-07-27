using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyShips.Models;

namespace MyShips.Services
{
    /// <summary>
    /// Class whose instance handles storing items in Azure.
    /// The URL for Azure Storage or other storage is needed.
    /// </summary>
    public class AzureDataStore : IDataStore<Item>
    {
        HttpClient client;
        IEnumerable<Item> items;

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Item>();
        }

        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }

        /// <summary>
        /// Gets one item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        /// <summary>
        /// Adds an item to the storage in Azure.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates an edited item using the async Http PUT command.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes an item from Azure Storage, if it exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}