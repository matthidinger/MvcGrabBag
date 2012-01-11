using System;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Metadata
{
    public abstract class MetadataAttribute : Attribute
    {
        public abstract void ApplyMetdata(ModelMetadata metadata);
    }
}