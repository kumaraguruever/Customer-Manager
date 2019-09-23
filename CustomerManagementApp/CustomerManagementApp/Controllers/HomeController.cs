using System.Web.Mvc;

namespace CustomerManagementApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return RedirectToAction("Index", "Customer");
            return View("");
        }
    }
}