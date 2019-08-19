using CustomerManagementApp.Controllers;
using CustomerManagementApp.Models;
using CustomerManagementApp.Models.API;
using CustomerManagementApp.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CustomerManagementApp.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {
        private Mock<IServiceRepository> __ServiceRepositoryMock = new Mock<IServiceRepository>();
        private List<Customer> __Customers = new List<Customer>();
        private HttpResponseMessage __HttpGetResponseMessage = new HttpResponseMessage();
        private HttpResponseMessage __HttpPostResponseMessage = new HttpResponseMessage();
        private CustomerController __CustomerController;

        private static Customer CreateCustomer(int customerID, string firstName, string surName,
            string city, string country, string emailAddress)
        {
            return new Customer()
            {
                CustomerID = customerID,
                FirstName = firstName,
                SurName = surName,
                City = city,
                Country = country,
                EmailAddress = emailAddress
            };
        }

        private void SetupServiceRepositoryGetResponseMock()
        {
            __ServiceRepositoryMock.Setup(repository => repository.GetResponse(It.IsAny<string>()))
                .Returns(__HttpGetResponseMessage);
        }

        private void SetupServiceRepositoryPostResponseMock()
        {
            __ServiceRepositoryMock.Setup(repository => repository.PostResponse(It.IsAny<string>(), It.Is<object>(customer => customer.GetType() == typeof(CustomerModel))))
                .Returns(__HttpPostResponseMessage);
        }

        private void SetupHttpGetResponseMessage(HttpStatusCode statusCode, object content)
        {
            __HttpGetResponseMessage.StatusCode = statusCode;
            __HttpGetResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(content), null, "application/json");
        }

        private void SetupHttpPostResponseMessage(HttpStatusCode statusCode, object content)
        {
            __HttpPostResponseMessage.StatusCode = statusCode;
            __HttpPostResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(content), null, "application/json");
        }

        [TestMethod]
        public async Task CustomerController_Details_ShouldReturnDetailsView()
        {
            Customer _Customer = CreateCustomer(1, "Mark", "Smith", "Birmingham", "United Kingdom", "Mark.Smith@gmail.com");
            SetupHttpGetResponseMessage(HttpStatusCode.OK, _Customer);
            SetupServiceRepositoryGetResponseMock();
            __CustomerController = new CustomerController(__ServiceRepositoryMock.Object);

            ViewResult _Result = await __CustomerController.Details("Enterprise","1") as ViewResult;

            Assert.IsNotNull(_Result);
            Customer _ResultCustomerModel = _Result.Model as Customer;
            Assert.IsNotNull(_ResultCustomerModel);
            Assert.AreEqual(_Customer.CustomerID, _ResultCustomerModel.CustomerID);
        }

        [TestMethod]
        public async Task CustomerController_Details_ShouldReturnBadRequestWhenIdIsNull()
        {
            Customer _Customer = CreateCustomer(1, "Mark", "Smith", "Birmingham", "United Kingdom", "Mark.Smith@gmail.com");
            SetupHttpGetResponseMessage(HttpStatusCode.OK, _Customer);
            SetupServiceRepositoryGetResponseMock();
            __CustomerController = new CustomerController(__ServiceRepositoryMock.Object);

            HttpStatusCodeResult _Result = await __CustomerController.Details(string.Empty, string.Empty) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, _Result.StatusCode);
        }

        [TestMethod]
        public async Task CustomerController_Details_ShouldReturnNotFoundWhenCustomerNotExists()
        {
            CustomerModel _Customer = null;
            SetupHttpGetResponseMessage(HttpStatusCode.NotFound, _Customer);
            SetupServiceRepositoryGetResponseMock();
            __CustomerController = new CustomerController(__ServiceRepositoryMock.Object);

            HttpNotFoundResult _Result = await __CustomerController.Details("Enterprise", "1") as HttpNotFoundResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, _Result.StatusCode);
        }

        [TestMethod]
        public async Task CustomerController_Index_ShouldReturnIndexView()
        {
            __Customers.Add(CreateCustomer(1, "Mark", "Smith", "Birmingham", "United Kingdom", "Mark.Smith@gmail.com"));
            __Customers.Add(CreateCustomer(2, "Rob", "Key", "New York", "United States of America", "Rob.Key@yahoo.com"));

            SetupHttpGetResponseMessage(HttpStatusCode.OK, __Customers);
            SetupServiceRepositoryGetResponseMock();
            __CustomerController = new CustomerController(__ServiceRepositoryMock.Object);

            ViewResult _Result = await __CustomerController.Index(string.Empty, "Birmingham", string.Empty, 1) as ViewResult;

            Assert.IsNotNull(_Result);
            IPagedList<CustomerModel> _ResultCustomerModel = _Result.Model as IPagedList<CustomerModel>;
            Assert.IsNotNull(_ResultCustomerModel);
            Assert.AreEqual(1, _ResultCustomerModel.Count);
            Assert.AreEqual(1, _ResultCustomerModel.First().CustomerID);
        }

        [TestMethod]
        public async Task CustomerController_Create_ShouldReturnNotCreateWhenEmailAddressAlreadyExists()
        {
            string _DuplicateEmailAddress = "Mark.Smith@gmail.com";
            __Customers.Add(CreateCustomer(1, "Mark", "Smith", "Birmingham", "United Kingdom", _DuplicateEmailAddress));
            SetupHttpGetResponseMessage(HttpStatusCode.OK, __Customers);
            SetupServiceRepositoryGetResponseMock();
            Customer _NewCustomer = CreateCustomer(2, "Rob", "Key", "New York", "United States of America", _DuplicateEmailAddress);
            SetupHttpPostResponseMessage(HttpStatusCode.OK, _NewCustomer);
            SetupServiceRepositoryPostResponseMock();
            __CustomerController = new CustomerController(__ServiceRepositoryMock.Object);

            ViewResult _Result = await __CustomerController.Create(_NewCustomer) as ViewResult;

            Assert.IsNotNull(_Result);
            Assert.IsTrue(_Result.ViewData.ModelState.Any(state => state.Value.Errors.First().ErrorMessage == "Email Address is already exists"));
            Customer _ResultCustomerModel = _Result.Model as Customer;
            Assert.IsNotNull(_ResultCustomerModel);
        }

        [TestMethod]
        public async Task CustomerController_Create_ShouldReturnNotCreateWhenCustomerDataIsValid()
        {
            __Customers.Add(CreateCustomer(1, "Mark", "Smith", "Birmingham", "United Kingdom", "Mark.Smith@gmail.com"));
            SetupHttpGetResponseMessage(HttpStatusCode.OK, __Customers);
            SetupServiceRepositoryGetResponseMock();
            Customer _NewCustomer = CreateCustomer(2, "Rob", "Key", "New York", "United States of America", "Rob.Key@yahoo.com");
            SetupHttpPostResponseMessage(HttpStatusCode.OK, _NewCustomer);
            SetupServiceRepositoryPostResponseMock();
            __CustomerController = new CustomerController(__ServiceRepositoryMock.Object);

            RedirectToRouteResult _Result = await __CustomerController.Create(_NewCustomer) as RedirectToRouteResult;

            Assert.IsNotNull(_Result);
            Assert.AreEqual("Index", _Result.RouteValues["action"]);  
        }
    }
}
