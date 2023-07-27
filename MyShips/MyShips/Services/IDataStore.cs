using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShips.Services
{
    /// <summary>
    /// Commin interface for implementing storage management whereever it s used.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
