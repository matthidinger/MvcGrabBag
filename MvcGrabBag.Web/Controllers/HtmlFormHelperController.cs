using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Controllers
{
    public class HtmlFormHelperController : Controller
    {
        [HttpGet]
        public ActionResult NewProduct()
        {
            var product = new ProductInput();
            return View(product);
        }

        [HttpPost]
        public ActionResult NewProduct(FormCollection form)
        {
            var product = new ProductInput();
            if(!TryUpdateModel(product, form))
            {
                return View(product);
            }

            // TODO: Would Save Product here

            return RedirectToAction("NewProduct");
        }

    }
}
