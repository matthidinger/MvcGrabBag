using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Controllers
{
    public class UploadedFileController : Controller
    {
        //
        // GET: /UploadedFile/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OpenFile(string fileName)
        {
            return File(fileName, "image/jpeg");
        }
    }
}
