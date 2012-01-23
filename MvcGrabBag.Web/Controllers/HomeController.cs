using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var categories = getCategories().Where(c => c.ParentId == null).ToList();

            return PartialView(categories);
        }

        private IEnumerable<Category> getCategories()
        {


            yield return new Category

                             {

                                 Name = "Garn",

                                 ShortDescription = "Vi har massor av garn",

                                 seName = "garn",

                                 SubCategories =

                                     {

                                         new Category

                                             {

                                                 Name = "Ullgarn",

                                                 ShortDescription = "Våra ullgarn är härliga",

                                                 seName = "ullgarn",

                                                 ParentId = 1,

                                             },

                                         new Category

                                             {

                                                 Name = "Bomullsgarn",

                                                 ShortDescription = "Våra bomullsgarn är härliga",

                                                 seName = "bomullsgarn",

                                                 ParentId = 1,

                                                 SubCategories =

                                                     {

                                                         new Category

                                                             {

                                                                 Name = "Stora bomullsgarn",

                                                                 ShortDescription = "Våra stora bomullsgarn är härliga",

                                                                 seName = "stora-bomullsgarn",

                                                                 ParentId = 3

                                                             }

                                                     }

                                             }

                                     }

                             };




            yield return new Category

            {

                Name = "Stickmönster",

                ShortDescription = "Våra stickmönster är snygga",

                seName = "stickmonster"

            };




            yield return new Category

            {

                Name = "Virkmönster",

                ShortDescription = "Våra virkmönster är snygga",

                seName = "virkmonster"

            };
        }
    }
}
