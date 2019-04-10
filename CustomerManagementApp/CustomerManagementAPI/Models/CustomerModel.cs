using System.ComponentModel.DataAnnotations;

namespace CustomerManagementApp.Models
{
    public class CustomerModel
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

        [Key]
        public int CustomerID { get; set; }


        [Required(ErrorMessage = "Please enter the Email Address")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Email Address in invalid")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter the First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Mobile Number")]
        [Phone(ErrorMessage ="Phone number is invalid")]
        public string MobileNumber { get; set; }

        [Display(Name = "Landline Number")]
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string LandlineNumber { get; set; }

        [Display(Name = "Surname")]
        public string SurName { get; set; }


        public string Title { get; set; }
    }
}