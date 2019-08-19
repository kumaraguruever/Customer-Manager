using Microsoft.WindowsAzure.Storage.Table;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CustomerManagementApp.Models
{
    public class CustomerModel : TableEntity
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int CustomerID { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string MobileNumber { get; set; }

        public string LandlineNumber { get; set; }
   
        public string SurName { get; set; }

        public string Title { get; set; }
    }
}