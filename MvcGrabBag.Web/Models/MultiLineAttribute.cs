using System.Web.Mvc;

namespace MvcGrabBag.Web.Models
{
    public class MultiLineAttribute : MetadataAttribute
    {
        public override void ApplyMetdata(ModelMetadata metadata)
        {
            metadata.TemplateHint = "MultiLine";
        }
    }
}