using CustomerManagementApp.Models;
using CustomerManagementApp.Models.API;
using CustomerManagementApp.Repository;
using CustomerManagementApp.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementApp.Controllers
{

    [RoutePrefix("customer")]
    [Route("{action = Index}")]
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class CustomerController : Controller
    {
        #region Class Variables...
        private IServiceRepository __ServiceRepository;
        #endregion

        #region Constants...
        private const string GET_ALL_CUSTOMER_URL = "api/Customer";
        private const string GET_CUSTOMER_URL = "api/Customer?{0}";
        private const string POST_CUSTOMER_URL = "api/Customer";
        private const string SEARCH_CUSTOMER_URL = "api/customer/search?{0}";

        private const int TOTAL_NO_OF_PAGES = 3;

        #endregion
        public CustomerController(IServiceRepository serviceRepository)
        {
            __ServiceRepository = serviceRepository ?? new ServiceRepository();        
        }
      
        private Uri RepositoryBaseAddress => new Uri(Request.Url.GetLeftPart(UriPartial.Authority));

        private void PopulateServiceRepositoryBaseAddress()
        {
            __ServiceRepository.BaseAddress = RepositoryBaseAddress;
        }
        [Route]
        public async Task<ActionResult> Index(string selectedSurname, string selectedCity, string selectedCountry, int? pageNumber)
        {
            List <Customer> _Customers = await FilterCustomers(selectedSurname, selectedCity, selectedCountry);
            _Customers = _Customers.OrderBy(customer => customer.FirstName)
                            .ThenBy(customer => customer.City)
                            .ThenBy(customer => customer.Country).ToList();
            return View(_Customers.ToPagedList(pageNumber ?? 1, TOTAL_NO_OF_PAGES));
        }

        private async Task<List<Customer>> FilterCustomers(string selectedSurname, string selectedCity, string selectedCountry)
        {
            PopulateServiceRepositoryBaseAddress();
            selectedCountry = !string.IsNullOrWhiteSpace(selectedCountry) && selectedCountry.ToLower() == "all" ? string.Empty : selectedCountry;
            string _QueryStrings = $"surname={selectedSurname}&city={selectedCity}&country={selectedCountry}";
            string _URL = string.Format(SEARCH_CUSTOMER_URL, _QueryStrings);

            HttpResponseMessage _HttpResponseMessage = __ServiceRepository.GetResponse(_URL);
            return  await _HttpResponseMessage.Content.ReadAsAsync<List<Customer>>();
         }

        private async Task<List<Customer>> GetAllCustomers()
        {
            PopulateServiceRepositoryBaseAddress();
            HttpResponseMessage _HttpResponseMessage = __ServiceRepository.GetResponse(GET_ALL_CUSTOMER_URL);
            List<Customer> _Customers = await _HttpResponseMessage.Content.ReadAsAsync<List<Customer>>();
            return _Customers;
        }

        public bool IsEmailExist(string emailAddress, string partitionKey, string rowKey)
        {
            List<Customer> _Customers = GetAllCustomers().Result;

            return _Customers.Any(customer => customer.EmailAddress.ToLower().Trim() == emailAddress.ToLower().Trim()
                                    && (customer.RowKey != rowKey));
        }

        // /Customer/5
        [HttpGet]
        [Route("{rowKey:minlength(1)}")]
        public async Task<ActionResult> Details(string partitionKey, string rowKey)
        {
            if (string.IsNullOrWhiteSpace(partitionKey) && string.IsNullOrWhiteSpace(rowKey))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customerModel = await GetCustomer(partitionKey, rowKey);

            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        private async Task<Customer> GetCustomer(string partitionKey, string rowKey)
        {
            string _URL = string.Format(GET_CUSTOMER_URL, GetIdentityQueryStrings(partitionKey, rowKey));
            Customer customerModel = await __ServiceRepository.GetResponse(_URL).Content.ReadAsAsync<Customer>();
            return customerModel;
        }

        private static string GetIdentityQueryStrings(string partitionKey, string rowKey) =>  $"partitionKey={partitionKey}&rowKey={rowKey}";
      
        // GET: Customer/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
       [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<ActionResult> Create([Bind(Include = "PartitionKey,RowKey,AddressLine1,AddressLine2,AddressLine3,City,Country,EmailAddress,FirstName,MobileNumber,LandlineNumber,SurName")] Customer customerModel)
        {
            ValidateUniqueEmailAddress(customerModel);
            if (ModelState.IsValid)
            {
              //  PopulateServiceRepositoryBaseAddress();
                await __ServiceRepository.PostResponse(POST_CUSTOMER_URL, customerModel).Content.ReadAsAsync<CustomerModel>();
                return RedirectToAction("Index");
            }

            return View(customerModel);
        }

        // GET: Customer/edit?partitionKey=a&rowKey=1
        [HttpGet]
        [Route("edit/{rowKey:minlength(1)}")]
        public async Task<ActionResult> Edit(string partitionKey, string rowKey)
        {
            if (string.IsNullOrWhiteSpace(partitionKey) && string.IsNullOrWhiteSpace(rowKey))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customerModel = await GetCustomer(partitionKey, rowKey);

            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: Customer/edit?partitionKey=a&rowKey=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{rowKey:minlength(1)}")]
        public async Task<ActionResult> Edit([Bind(Include = "PartitionKey,RowKey,AddressLine1,AddressLine2,AddressLine3,City,Country,EmailAddress,FirstName,MobileNumber,LandlineNumber,SurName")] Customer customerModel)
        {
            ValidateUniqueEmailAddress(customerModel);

            if (ModelState.IsValid)
            {
              //  PopulateServiceRepositoryBaseAddress();
                string _URL = string.Format(GET_CUSTOMER_URL, GetIdentityQueryStrings(customerModel.PartitionKey, customerModel.RowKey));
                Customer _CustomerModel = await __ServiceRepository.PutResponse(_URL, customerModel).Content.ReadAsAsync<Customer>();

                return RedirectToAction("Index");
            }
            return View(customerModel);
        }

        private void ValidateUniqueEmailAddress(Customer customerModel)
        {
            if (IsEmailExist(customerModel.EmailAddress, customerModel.PartitionKey, customerModel.RowKey))
            {
                ModelState.AddModelError("EmailAddress", "Email Address is already exists");
            }
        }

        // GET: Customer/Delete?partitionKey=a&rowKey=1
        [HttpGet]
        [Route("delete/{rowKey:minlength(1)}")]
        public async Task<ActionResult> Delete(string partitionKey, string rowKey)
        {
            if (string.IsNullOrWhiteSpace(partitionKey) && string.IsNullOrWhiteSpace(rowKey))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customerModel = await GetCustomer(partitionKey, rowKey);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: Customer/Delete?partitionKey=a&rowKey=1
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteConfirmed(string partitionKey, string rowKey)
        {
            string _URL = string.Format(GET_CUSTOMER_URL, GetIdentityQueryStrings(partitionKey, rowKey));

            await __ServiceRepository.DeleteResponse(_URL).Content.ReadAsAsync<CustomerModel>();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}
