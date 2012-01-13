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


        public ActionResult Bad()
        {
            var firstVisit = HttpContext.Cache.Get("FirstVisit") as DateTime?;
            if (firstVisit == null)
            {
                firstVisit = DateTime.Now;
                HttpContext.Cache.Insert("FirstVisit", firstVisit, null, DateTime.Now.AddMinutes(1), TimeSpan.Zero);
            }

            return View("Index", firstVisit.Value);
        }

        public ActionResult Index()
        {
            var firstVisit = _cache.Get(CacheScope.User, "FirstVisit", TimeSpan.FromMinutes(1), () => DateTime.Now);
            return View(firstVisit);
        }

        public ActionResult FullyAbstracted()
        {
            var firstVisit = AppCache.UsersFirstVisit;
            return View("Index", firstVisit);
        }

    }

    /// <summary>
    /// This class demonstrates fully abstracting the details of your caching strategy and could serve as the single entry point for cached data
    /// </summary>
    public static class AppCache
    {
        public static ICache InternalCache = new HttpCache();

        public static DateTime UsersFirstVisit = InternalCache.Get(CacheScope.User, "FirstVisit", TimeSpan.FromMinutes(1), () => DateTime.Now);
    }
}
