
using Microsoft.WindowsAzure.Storage.Table;

namespace CustomerManagementApp.Models.Client
{
    public interface ICloudStorageClient
    {
        CloudTable GetTableReference(string tableName);
        CloudTableClient CloudTableClient { get; }
    }
}