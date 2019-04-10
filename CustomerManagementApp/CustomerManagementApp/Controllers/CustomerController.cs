using CustomerManagementApp.Models;
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
using System.Web.Mvc;

namespace CustomerManagementApp.Controllers
{

    [RoutePrefix("customer")]
    [Route("{action = Index}")]
    [HandleError(ExceptionType = typeof(Exception), View = "NotFound")]
    public class CustomerController : Controller
    {
        #region Class Variables...
        private IServiceRepository __ServiceRepository;
        #endregion

        #region Constants...
        private const string GET_ALL_CUSTOMER_URL = "api/Customer";
        private const string GET_CUSTOMER_URL = "api/Customer/{0}";
        private const string POST_CUSTOMER_URL = "api/Customer";
        private const int TOTAL_NO_OF_PAGES = 3;

        #endregion
        public CustomerController(IServiceRepository serviceRepository)
        {
            __ServiceRepository = serviceRepository ?? new ServiceRepository(new System.Net.Http.HttpClient());
        }
        private CustomerDBContext db = new CustomerDBContext();

        [Route]
        public async Task<ActionResult> Index(string selectedSurname, string selectedCity, string selectedCountry, int? pageNumber)
        {
         
            List < CustomerModel > _Customers = await FilterCustomers(selectedSurname, selectedCity, selectedCountry);
            _Customers = _Customers.OrderBy(customer => customer.FirstName)
                            .ThenBy(customer => customer.City)
                            .ThenBy(customer => customer.Country).ToList();
            return View(_Customers.ToPagedList(pageNumber ?? 1, TOTAL_NO_OF_PAGES));
        }

        private async Task<List<CustomerModel>> FilterCustomers(string selectedSurname, string selectedCity, string selectedCountry)
        {
            List<CustomerModel> _FilteredCustomers = await GetAllCustomers();
            if (!string.IsNullOrEmpty(selectedSurname))
            {
                _FilteredCustomers = _FilteredCustomers
                                    .Where(customer => customer.SurName.ToLower().StartsWith(selectedSurname.ToLower()))
                                    .ToList();
            }

            if (!string.IsNullOrEmpty(selectedCity))
            {
                _FilteredCustomers = _FilteredCustomers
                                    .Where(customer => customer.City.ToLower().StartsWith(selectedCity.ToLower()))

                                    .ToList();
            }
            if (!string.IsNullOrEmpty(selectedCountry) && selectedCountry != DataHelper.ALL_VALUE)
            {
                _FilteredCustomers = _FilteredCustomers
                                 .Where(customer => customer.Country.ToLower() == selectedCountry.ToLower())
                                 .ToList();
            }

            return _FilteredCustomers;
        }

        private async Task<List<CustomerModel>> GetAllCustomers()
        {
            HttpResponseMessage _HttpResponseMessage = __ServiceRepository.GetResponse(GET_ALL_CUSTOMER_URL);
            List<CustomerModel> _Customers = await _HttpResponseMessage.Content.ReadAsAsync<List<CustomerModel>>();
            return _Customers;
        }

        public bool IsEmailExist(string emailAddress, int? ID)
        {
            List<CustomerModel> _Customers = GetAllCustomers().Result;

            return _Customers.Any(customer => customer.EmailAddress == emailAddress
                                    && customer.CustomerID != ID);
        }

        // /Customer/5
        [Route("{id:int:min(1)}")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = await GetCustomer(id);

            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        private async Task<CustomerModel> GetCustomer(int? id)
        {
            string _URL = string.Format(GET_CUSTOMER_URL, id);
            CustomerModel customerModel = await __ServiceRepository.GetResponse(_URL).Content.ReadAsAsync<CustomerModel>();
            return customerModel;
        }

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
        public async Task<ActionResult> Create([Bind(Include = "CustomerID,AddressLine1,AddressLine2,AddressLine3,City,Country,EmailAddress,FirstName,MobileNumber,LandlineNumber,SurName")] CustomerModel customerModel)
        {
            ValidateUniqueEmailAddress(customerModel);
            if (ModelState.IsValid)
            {
                await __ServiceRepository.PostResponse(POST_CUSTOMER_URL, customerModel).Content.ReadAsAsync<CustomerModel>();
                return RedirectToAction("Index");
            }

            return View(customerModel);
        }

        // GET: Customer/edit/5
        [HttpGet]
        [Route("edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = await GetCustomer(id);

            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: Customer/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerID,AddressLine1,AddressLine2,AddressLine3,City,Country,EmailAddress,FirstName,MobileNumber,LandlineNumber,SurName")] CustomerModel customerModel)
        {
            ValidateUniqueEmailAddress(customerModel);

            if (ModelState.IsValid)
            {
                string _URL = string.Format(GET_CUSTOMER_URL, customerModel.CustomerID);
                CustomerModel _CustomerModel = await __ServiceRepository.PutResponse(_URL, customerModel).Content.ReadAsAsync<CustomerModel>();

                return RedirectToAction("Index");
            }
            return View(customerModel);
        }

        private void ValidateUniqueEmailAddress(CustomerModel customerModel)
        {
            if (IsEmailExist(customerModel.EmailAddress, customerModel.CustomerID))
            {
                ModelState.AddModelError("EmailAddress", "Email Address is already exists");
            }
        }

        // GET: Customer/Delete/5
        [HttpGet]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerModel customerModel = await GetCustomer(id);
            if (customerModel == null)
            {
                return HttpNotFound();
            }
            return View(customerModel);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string _URL = string.Format(GET_CUSTOMER_URL, id);
            await __ServiceRepository.DeleteResponse(_URL).Content.ReadAsAsync<CustomerModel>();
            return RedirectToAction("Index");
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
