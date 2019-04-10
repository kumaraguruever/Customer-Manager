using CustomerManagementApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace CustomerManagementApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            RedirectToRouteResult _Result = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(_Result);
            Assert.AreEqual("Index", _Result.RouteValues["action"]);
        }
    }
}
