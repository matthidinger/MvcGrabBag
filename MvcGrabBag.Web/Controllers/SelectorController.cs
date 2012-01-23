using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Controllers
{
    public class SelectorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new DisplayModeOptions();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(DisplayModeOptions input)
        {
            if (!ModelState.IsValid)
                return View(input);

            return RedirectToAction("Index");
        }
    }
}
