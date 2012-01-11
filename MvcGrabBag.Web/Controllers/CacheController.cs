using System;
using System.Web.Mvc;
using MvcGrabBag.Web.Caching;

namespace MvcGrabBag.Web.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICache _cache;

        public CacheController() : this(new HttpCache())
        {
        }

        public CacheController(ICache cache)
        {
            _cache = cache;
        }

        public ActionResult Index()
        {
            var now = _cache.Get(CacheScope.User, "Now", 1, () => DateTime.Now);
            return View(now);
        }

    }
}
