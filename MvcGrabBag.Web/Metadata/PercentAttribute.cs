using System;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Metadata
{
    public class PercentAttribute : Attribute, IMetadataAware
    {
        public decimal Increment { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.TemplateHint = "Percent";
            metadata.AdditionalValues["PercentIncrement"] = Increment;
        }
    }
}