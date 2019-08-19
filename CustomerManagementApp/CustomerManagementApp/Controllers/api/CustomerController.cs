using CustomerManagementApp.Adapter;
using CustomerManagementApp.Models;
using CustomerManagementApp.Models.API;
using CustomerManagementApp.Models.Client;
using CustomerManagementApp.Models.CloudStorage.Repository;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Wolnik.Azure.TableStorage.Repository;

namespace CustomerManagementApp.Controllers.api
{
    public class CustomerController : ApiController
    {
        private CustomerDBContext db = new CustomerDBContext();

        private IAzureTableStorage __AzureTableStorage;

        private const string CUSTOMER_TABLE_NAME = "Customer";

        public CustomerController()
        {
            __AzureTableStorage = new AzureTableStorage(string.Empty, new CloudStorageClient());
        }
        // GET: api/Customer
        [Route("api/customer/")]
        public IList<Customer> GetCustomer()
        {
          return  __AzureTableStorage.GetAllAsync<CustomerModel>(CUSTOMER_TABLE_NAME).Result.ToList().ConvertTo();
   
        }

        [HttpGet]
        [Route("api/customer/search")]
        // GET: api/Customer
        public IList<Customer> SearchCustomer([FromUri]string surname, [FromUri]string city, [FromUri]string country)
        {
            if(string.IsNullOrWhiteSpace(surname) && string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(country))
            {
                return GetCustomer();
            }
            string surnameFilter = TableQuery.GenerateFilterCondition("SurName", QueryComparisons.Equal, surname);
            string cityFilter = TableQuery.GenerateFilterCondition("City", QueryComparisons.Equal, city);
            string countryFilter = TableQuery.GenerateFilterCondition("Country", QueryComparisons.Equal, country);

            string _ResultFilter = TableQuery.CombineFilters(surnameFilter, TableOperators.Or, cityFilter);
            _ResultFilter = TableQuery.CombineFilters(_ResultFilter, TableOperators.Or, countryFilter);
            TableQuery<CustomerModel> _CustomerQuery = new TableQuery<CustomerModel>().Where(_ResultFilter);
            return __AzureTableStorage.QueryAsync<CustomerModel>(CUSTOMER_TABLE_NAME, _CustomerQuery).Result.ToList().ConvertTo();

        }
        // GET: api/Customer?partitionKey=a&rowKey=1
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomerModel([FromUri] string partitionKey, [FromUri] string rowKey)
        {
            CustomerModel customerModel = await __AzureTableStorage.GetAsync<CustomerModel>(CUSTOMER_TABLE_NAME, partitionKey, rowKey);
            if (customerModel == null)
            {
                return NotFound();
            }

            return Ok(customerModel.ConvertTo());
        }

        // PUT: api/Customer?partitionKey=a&rowKey=1
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomerModel(string partitionKey, string rowKey, Customer customerModel)
        {

            ValidateUniqueEmailAddress(customerModel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (partitionKey != customerModel.PartitionKey || rowKey != customerModel.RowKey)
            {
                return BadRequest();
            }

            await __AzureTableStorage.AddOrUpdateAsync(CUSTOMER_TABLE_NAME, customerModel.ConvertTo());
            return StatusCode(HttpStatusCode.NoContent);
        }
        public bool IsEmailExist(string emailAddress, string partitionKey, string rowKey)
        {
            IList<Customer> _Customers = GetCustomer();

            return _Customers.Any(customer => customer.EmailAddress == emailAddress
                                    && customer.PartitionKey != partitionKey
                                    && customer.RowKey != rowKey);
        }

        private void ValidateUniqueEmailAddress(Customer customerModel)
        {
            if (IsEmailExist(customerModel.EmailAddress, customerModel.PartitionKey, customerModel.RowKey))
            {
                ModelState.AddModelError("EmailAddress", "Email Address is already exists");
            }
        }

        // POST: api/Customer
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> PostCustomerModel(Customer customerModel)
        {
            ValidateUniqueEmailAddress(customerModel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          CustomerModel _CustomerModel =  await __AzureTableStorage.AddOrUpdateAsync(CUSTOMER_TABLE_NAME, customerModel.ConvertTo()) as CustomerModel;


            return CreatedAtRoute("DefaultApi", new { partitionKey = customerModel.PartitionKey, rowKey = customerModel.RowKey  }, _CustomerModel.ConvertTo());
        }

        // DELETE: api/Customer/5
        [HttpDelete]
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> DeleteCustomerModel(string partitionKey, string rowKey)
        {
            CustomerModel customerModel = await __AzureTableStorage.GetAsync<CustomerModel>(CUSTOMER_TABLE_NAME, partitionKey, rowKey);
            if (customerModel == null)
            {
                return NotFound();
            }

           await __AzureTableStorage.DeleteAsync(CUSTOMER_TABLE_NAME, customerModel);

            return Ok(customerModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}