using System.Web.Mvc;

namespace CustomerManagementApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag["ErrorMessage"] = HttpContext.Error.Message;
            return View();
        }

        public ActionResult NotFound()
        {
            ViewBag["ErrorMessage"] = HttpContext.Error.Message;
            return View();
        }
    }
}