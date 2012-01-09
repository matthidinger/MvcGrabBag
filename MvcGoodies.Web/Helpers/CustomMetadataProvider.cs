using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using MvcGoodies.Web.Models;

namespace MvcGoodies.Web.Helpers
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

            //var currencyAttribute = attributes.OfType<CurrencyAttribute>().FirstOrDefault();
            //if (currencyAttribute != null)
            //{
            //    metadata.TemplateHint = "Currency";
            //    metadata.AdditionalValues.Add("DisplayCents", currencyAttribute.DisplayCents);
            //    metadata.AdditionalValues.Add("Increment", currencyAttribute.Increment);

            //    metadata.EditFormatString = "{0:C}";
            //    //if (currencyAttribute.DisplayCents)
            //    //    metadata.EditFormatString = "{0:0.C00}";
            //}

            //var phoneAttribute = attributes.OfType<PhoneAttribute>().FirstOrDefault();
            //if (phoneAttribute != null)
            //{
            //    metadata.TemplateHint = "Phone";
            //}

            //var percentAttribute = attributes.OfType<PercentAttribute>().FirstOrDefault();
            //if (percentAttribute != null)
            //{
            //    metadata.TemplateHint = "Percent";
            //    metadata.AdditionalValues.Add("Increment", percentAttribute.Increment);
            //}


            var multiLineAttribute = attributes.OfType<MultiLineAttribute>().FirstOrDefault();
            if (multiLineAttribute != null)
            {
                metadata.TemplateHint = "MultiLine";
            }


            //// Handle runtime metadata
            //var container = GetContainer(modelAccessor);
            //var metadataContainer = container as IMetadataContainer;
            //if (metadataContainer != null)
            //{
            //    ((IMetadataAdapter)metadataContainer.Metadata).ApplyMetadata(metadata);
            //}



            //// Selectors
            //if (typeof(SelectorInput).IsAssignableFrom(modelType))
            //{
            //    var selector = (SelectorInput)modelAccessor();
            //    if (selector == null)
            //    {
            //        throw new InvalidOperationException(
            //            "The selector being bound is null, this should not happen. Did you forget to new it up in the view model?");
            //    }

            //    if (metadata.IsReadOnly)
            //        selector.IsRequired = false;

            //    metadata.IsRequired = selector.IsRequired;

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

    public class PercentAttribute : Attribute
    {
        public decimal Increment { get; set; }
    }
}