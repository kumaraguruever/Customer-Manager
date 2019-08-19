using CustomerManagementApp.Models;
using CustomerManagementApp.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagementApp.Adapter
{
    public static class CustomerAdapter
    {
        public static Customer ConvertTo(this CustomerModel customerModel)
        {
            if (customerModel == null)
            {
                throw new ArgumentNullException("Customer model should not be null");
            }

            Customer _Customer = new Customer()
            {
                AddressLine1 = customerModel.AddressLine1,
                AddressLine2 = customerModel.AddressLine2,
                AddressLine3 = customerModel.AddressLine3,
                City = customerModel.City,
                Country = customerModel.Country,
                EmailAddress = customerModel.EmailAddress,
                FirstName = customerModel.FirstName,
                MobileNumber = customerModel.MobileNumber,
                LandlineNumber = customerModel.LandlineNumber,
                PartitionKey = customerModel.PartitionKey,
                RowKey = customerModel.RowKey,
                SurName = customerModel.SurName,
                Title = customerModel.Title
            };
            return _Customer;
        }
        public static List<Customer> ConvertTo(this IEnumerable<CustomerModel> customerModels)
        {
            List<Customer> _Customers = new List<Customer>();
            customerModels.ToList().ForEach(model => _Customers.Add(model.ConvertTo()));
            return _Customers;
        }

        public static CustomerModel ConvertTo(this Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("Customer  should not be null");
            }

            CustomerModel _Customer = new CustomerModel()
            {
                AddressLine1 = customer.AddressLine1,
                AddressLine2 = customer.AddressLine2,
                AddressLine3 = customer.AddressLine3,
                City = customer.City,
                Country = customer.Country,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                MobileNumber = customer.MobileNumber,
                LandlineNumber = customer.LandlineNumber,
                PartitionKey = customer.PartitionKey,
                RowKey = customer.RowKey,
                SurName = customer.SurName,
                Title = customer.Title
            };
            return _Customer;
        }
    }
}