using System.Linq;
using System.Web.Mvc;
using MvcGrabBag.Web.EntityFramework;

namespace MvcGrabBag.Web.Controllers
{
    public class EFController : Controller
    {
        private readonly IDataContext _db;

        public EFController(IDataContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            // _db.Categories will only return categories where IsDeleted == false
            var categories = _db.Categories;
            var unfiltered = _db.Categories.Unfiltered();
            
            return Content(string.Format("Found {0} active categories. Found {1} total.", categories.Count(), unfiltered.Count()));
        }
    }
}
