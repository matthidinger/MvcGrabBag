using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using MvcGrabBag.Web.Helpers;

namespace MvcGrabBag.Web.Metadata
{
    public class CustomMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName)
        {
            attributes = attributes.ToList();
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);


            if (propertyName == null)
                return metadata;

            if (modelType.IsAssignableFrom(typeof(DateTime?)))
            {
                metadata.EditFormatString = "{0:d}";
            }


            var displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute == null || displayAttribute.Name == null)
            {
                metadata.DisplayName = propertyName.Wordify();
            }

  
            //// Handle runtime metadata
            //var container = GetContainer(modelAccessor);
            //var metadataContainer = container as IMetadataContainer;
            //if (metadataContainer != null)
            //{
            //    ((IMetadataAdapter)metadataContainer.Metadata).ApplyMetadata(metadata);
            //}


            // !! Make sure this is always at the bottom
            if (metadata.IsReadOnly && !metadata.IsComplexType)
            {
                metadata.AdditionalValues["OriginalTemplateHint"] = metadata.TemplateHint;
                metadata.TemplateHint = "ReadOnly";
            }

            return metadata;
        }

        private static object GetContainer(Func<object> modelAccessor)
        {
            if (modelAccessor == null) return null;

            object target = modelAccessor.Target;
            var containerField = target.GetType().GetField("container");
            if (containerField != null)
            {
                object container = containerField.GetValue(target);
                return container;
            }

            return null;
        }
    }
}