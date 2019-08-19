using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagementApp.Models.CloudStorage.Repository;
using Microsoft.WindowsAzure.Storage.Table;

namespace Wolnik.Azure.TableStorage.Repository
{
    public interface IAzureTableStorage
    {
        Task<object> AddAsync(string tableName, ITableEntity entity);
        Task<IEnumerable<T>> AddBatchAsync<T>(string tableName, IEnumerable<ITableEntity> entities, BatchOperationOptions options = null) where T : class, ITableEntity, new();
        Task<object> AddOrUpdateAsync(string tableName, ITableEntity entity);
        Task<object> DeleteAsync(string tableName, string partitionKey, string rowKey);
        Task<object> DeleteAsync(string tableName, ITableEntity entity);
        Task<IEnumerable<T>> GetAllAsync<T>(string tableName) where T : class, ITableEntity, new();
        Task<T> GetAsync<T>(string tableName, string partitionKey, string rowKey) where T : class, ITableEntity;
        Task<IEnumerable<T>> QueryAsync<T>(string tableName, TableQuery<T> query) where T : class, ITableEntity, new();
        Task<object> UpdateAsync(string tableName, ITableEntity entity);
    }
}