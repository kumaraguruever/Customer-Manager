//using Microsoft.Azure.Cosmos.Table;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace CustomerManagementApp.Models.CloudStorage.Repository
//{
//    public class TableRepository<TEntity> : ITableRepository<TEntity> where TEntity: class, ITableEntity
//    {
//        private CloudTable __CloudTable;

//        public TableRepository(CloudTable clouldTable)
//        {
//            __CloudTable = clouldTable ?? throw new ArgumentNullException("Cloud Table should not be null");
//        }

//        public void CreateOrUpdate(TEntity entity)
//        {
//            TableOperation _SaveOperation = TableOperation.InsertOrReplace(entity);
//            __CloudTable.Execute(_SaveOperation);
//        }

//        public void Delete(TEntity entity)
//        {
//            TableOperation _DeleteOperation = TableOperation.Delete(entity);
//            __CloudTable.Execute(_DeleteOperation);

//        }

//        //public IEnumerable<TEntity> GetAll()
//        //{
//        //    TableQuery query = new TableQuery();
//        //    return __CloudTable.ExecuteQuery(query);
//        //}

//        //public virtual TEntity GetByID(object id)
//        //{
//        //    var query = new TableQuery().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString()));
//        //    var result = __CloudTable.ExecuteQuery(query).First();
//        //    return result;
//        //}

//        public TEntity GetRow(string partitionKey, string rowKey)
//        {
//            TableOperation _RetrieveOperation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
//            TableResult _Result = __CloudTable.Execute(_RetrieveOperation);
//            return _Result.Result as TEntity;
//        }
//    }
//}