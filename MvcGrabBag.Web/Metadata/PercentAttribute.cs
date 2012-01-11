using System.Web.Mvc;

namespace MvcGrabBag.Web.Metadata
{
    public class PercentAttribute : MetadataAttribute
    {
        public decimal Increment { get; set; }

        public override void ApplyMetdata(ModelMetadata metadata)
        {
            metadata.TemplateHint = "Percent";
            metadata.AdditionalValues["PercentIncrement"] = Increment;
        }
    }
}