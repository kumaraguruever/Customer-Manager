using System.Collections.Generic;
using System.Data.Entity;

namespace CustomerManagementApp.Models
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext() : base("name=CustomerDBContext")
        {
            Database.SetInitializer<CustomerDBContext>(new CustomerDBInitializer<CustomerDBContext>());
        }

        public virtual DbSet<CustomerModel> Customers { get; set; }
        private class CustomerDBInitializer<T> : DropCreateDatabaseAlways<CustomerDBContext>
        {

            private static CustomerModel CreateCustomer (string firstname, string surname, string emailAddress, 
                string addressLine1, string addressLine2, string addressLine3, string city, string country, 
                string landlineNumber, string mobileNumber)
            {
                return new CustomerModel()
                {
                    FirstName = firstname,
                    SurName = surname,
                    EmailAddress =emailAddress,
                    AddressLine1 =addressLine1,
                    AddressLine2 = addressLine2,
                    AddressLine3 = addressLine3,
                    City = city,
                    Country = country,
                    LandlineNumber =landlineNumber,
                    MobileNumber = mobileNumber
                };
            }
            protected override void Seed(CustomerDBContext context)
            {

                IList<CustomerModel> _Customers = new List<CustomerModel>();
                _Customers.Add(CreateCustomer("David", "Hyland", "david.hyland@gmail.com", "42 Smith Rock Road",
                    "Greenway", string.Empty, "Cape Town", "South Africa", "123-44-7774", "974-4738-52"));

                _Customers.Add(CreateCustomer("Richard", "Kevin", "richard.kevin@yahoo.com", "5 Langham Close",
                  "Alderbrook", "Kensilton", "Paris", "France", "623-44-7774", "894-4738-52"));

                _Customers.Add(CreateCustomer("Kelly", "Taylor", "Kelly.Taylor@gmail.com", "54 CostWold way",
                                  string.Empty, string.Empty, "Birmingham", "United Kingdom", "123-44-7774", "774-4738-52"));

                _Customers.Add(CreateCustomer("Chris", "Devey", "chris.devey@live.com", "42 Kendall Road",
                                  "Greenway", string.Empty, "Birmingham", "United Kingdom", "111-44-7774", "777-6778-52"));

                _Customers.Add(CreateCustomer("Jamie", "Mansel", "jamie.mansel@gmail.com", "42 shuttle Road",
                                  "Greenway", "Northfield cross", "New york", "United States", "123-48593-7774", "474-4738-52"));

                _Customers.Add(CreateCustomer("Tina", "Thompson", "tina.thompson@gmail.com", "24 Sixers Path",
                    "North Road", "Brigter side", "London", "United Kingdom", "123-28-7774", "774-6473-52"));


                foreach (CustomerModel _Customer in _Customers)
                    context.Customers.Add(_Customer);
                base.Seed(context);
            }
        }
    }
}