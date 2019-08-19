using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementApp.Models.API
{
    public class Customer
    {
        [Required(ErrorMessage = "Please enter the Address Line 1")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line 3")]
        public string AddressLine3 { get; set; }


        [Required(ErrorMessage = "Please enter the City")]
        [Display(Name = "City")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please enter the Country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public int CustomerID { get; set; }


        [Required(ErrorMessage = "Please enter the Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address in invalid")]
        // [Remote("IsEmailExist", "Customer", AdditionalFields ="CustomerID", ErrorMessage ="Email Address already exists")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter the First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Mobile Number")]
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string MobileNumber { get; set; }

        [Display(Name = "Landline Number")]
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string LandlineNumber { get; set; }

        public string PartitionKey { get; set; } = "Enterprise";

        public string RowKey { get; set; }

        [Display(Name = "Surname")]
        public string SurName { get; set; }


        public string Title { get; set; }
    }
}