using System.IO;
using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Controllers
{
    public class UploadedFileController : Controller
    {
        [HttpGet]
        public ActionResult Index(string fileName)
        {
            var model = new ProductThumbnail(fileName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string fileName, FormCollection form)
        {
            var model = new ProductThumbnail(fileName);
            if (!TryUpdateModel(model, form))
                return View(model);

            if(model.Thumbnail.HasNewFile)
            {
                var newFile = model.Thumbnail.NewFile;
                fileName = Path.GetFileName(newFile.FileName);
                using(var file = new FileStream(Path.Combine(Path.GetTempPath(), fileName), FileMode.Create))
                {
                    newFile.OpenStream().CopyTo(file);
                }
            }

            return RedirectToAction("Index", new{ fileName = Path.GetFileName(fileName)});
        }

        public ActionResult OpenFile(string fileName)
        {
            return File(Path.Combine(Path.GetTempPath(), fileName), "image/jpeg");
        }
    }
}
