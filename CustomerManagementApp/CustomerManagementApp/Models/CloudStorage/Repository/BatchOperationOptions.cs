using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementApp.Models.CloudStorage.Repository
{
    public class BatchOperationOptions
    {
        public BatchInsertMethod BatchInsertMethod { get; set; }
    }

    public enum BatchInsertMethod
    {
        Insert,
        InsertOrReplace,
        InsertOrMerge
    }
}