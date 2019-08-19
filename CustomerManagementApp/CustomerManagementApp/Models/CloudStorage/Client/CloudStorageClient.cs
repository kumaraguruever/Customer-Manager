//using Microsoft.Azure.Cosmos.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CustomerManagementApp.Models.Client
{
    public class CloudStorageClient : ICloudStorageClient
    {
        private CloudTableClient __CloudTableClient;
        public CloudStorageClient()
        {
            CloudStorageAccount _CloudStorageAccount = CloudStorageAccount.Parse(
                                        ConfigurationManager.AppSettings["AzureStorageConnectionStrings"]);
            __CloudTableClient = _CloudStorageAccount.CreateCloudTableClient();
        }

        public CloudTable GetTableReference(string tableName)
        {
            CloudTable _Table = __CloudTableClient.GetTableReference(tableName);
            _Table.CreateIfNotExistsAsync();
            return _Table;
        }
        public CloudTableClient CloudTableClient => __CloudTableClient;
    }
}