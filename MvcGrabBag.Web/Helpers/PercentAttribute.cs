using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Helpers
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