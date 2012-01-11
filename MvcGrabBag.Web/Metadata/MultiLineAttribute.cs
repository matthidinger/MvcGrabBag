using System.Web.Mvc;

namespace MvcGrabBag.Web.Metadata
{
    public class MultiLineAttribute : MetadataAttribute
    {
        public override void ApplyMetdata(ModelMetadata metadata)
        {
            metadata.TemplateHint = "MultiLine";
        }
    }
}