using System.Web;
using System.Web.Mvc;

namespace MvcGrabBag.Web.FileUpload
{
    public class UploadedFileModelBinder : IModelBinder
    {
        private readonly HttpPostedFileBaseModelBinder _postedFileBaseModelBinder = new HttpPostedFileBaseModelBinder();
        private readonly IModelBinder _defaultBinder = ModelBinders.Binders.DefaultBinder;

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = (UploadedFile) _defaultBinder.BindModel(controllerContext, bindingContext);
            var newFileBindingContext = new ModelBindingContext(bindingContext)
                                            {
                                                ModelName = bindingContext.ModelName + ".NewFile"
                                            };

            var postedFile = _postedFileBaseModelBinder.BindModel(controllerContext, newFileBindingContext) as HttpPostedFileBase;

            if (bindingContext.ModelMetadata.IsRequired)
            {
                if ((postedFile == null || postedFile.ContentLength == 0) && model.ContentLength == 0)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Please select a file to upload");
                }
            }

            if (postedFile != null)
            {
                model.NewFile = new UploadedFile(postedFile);
            }

            return model;
        }
    }
}