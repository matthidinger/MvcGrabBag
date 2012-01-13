using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcGrabBag.Web.FileUpload
{
    public class UploadedFileModelValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            if(metadata.ModelType == typeof(UploadedFile))
                yield return new UploadedFileModelValidator(metadata, context);
        }
    }

    public class UploadedFileModelValidator : ModelValidator
    {
        public UploadedFileModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext)
        {
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            yield break;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule()
                             {
                                 ErrorMessage = "Required",
                                 ValidationType = "requiredfileupload",
                             };
        }
    }
}