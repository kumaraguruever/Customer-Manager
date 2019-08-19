using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementApp.Models.CloudStorage.Repository
{
    public interface ITableRepository<TEntity> where TEntity : class, ITableEntity
    {
        // Pass TableEntity as Generic type
        // Create methods like GetAll, GetAll with filter, Get by Partition Key and Row Key, Create, Update and Delete

        IEnumerable<TEntity> GetAll();
        void CreateOrUpdate(TEntity entity);
        void Delete(TEntity entity);
        TEntity GetRow(string partitionKey, string rowKey);
    }
}