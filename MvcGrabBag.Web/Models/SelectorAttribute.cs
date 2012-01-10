using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Models
{
    public abstract class SelectorAttribute : MetadataAttribute
    {
        protected SelectorAttribute()
        {
            MaxRadioButtons = 6;
            OptionLabel = "-- Select One --";
        }

        public abstract IEnumerable<SelectListItem> GetOptions();


        public string OptionLabel { get; set; }
        public int MaxRadioButtons { get; set; }
        public bool AllowMultiple { get; set; }

        public override void ApplyMetdata(ModelMetadata metadata)
        {
            if(typeof (System.Collections.IEnumerable).IsAssignableFrom(metadata.ModelType))
            {
                AllowMultiple = true;
            }

            var selectorModel = new Selector
                                    {
                                        OptionLabel = OptionLabel,
                                        RadioButtonThreshold = MaxRadioButtons,
                                        AllowMultipleSelection = AllowMultiple,
                                        Items = GetOptions().ToList(),
                                    };

            metadata.TemplateHint = "Selector";
            metadata.AdditionalValues["SelectorModelMetadata"] = selectorModel;
        }


    }

    public abstract class MetadataAttribute : Attribute
    {
        public abstract void ApplyMetdata(ModelMetadata metadata);
    }
}