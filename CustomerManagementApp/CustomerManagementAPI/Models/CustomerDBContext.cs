using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Configuration;

namespace CustomerManagementAPI.Models
{
    public class CustomerDBContext : WebContext
    {
        public ISet<CustomerModel> Customer { get; set; }
    }
}