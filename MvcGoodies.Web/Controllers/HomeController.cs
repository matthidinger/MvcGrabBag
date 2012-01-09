using System.Web.Mvc;
using MvcGoodies.Web.Models;

namespace MvcGoodies.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var product = new ProductInput();
            return View(product);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var product = new ProductInput();
            if(!TryUpdateModel(product, form))
            {
                return View(product);
            }

            return RedirectToAction("Index");
        }

    }
}
