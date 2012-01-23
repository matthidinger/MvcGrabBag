using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Selectors
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class SelectorAttribute : Attribute, IMetadataAware
    {
        protected SelectorAttribute()
        {
            BulkSelectionThreshold = 6;
            OptionLabel = "-- Select One --";
        }

        /// <summary>
        /// Get the list of items that the user can choose from. This method must be overridden in derived classes.
        /// </summary>
        public abstract IEnumerable<SelectListItem> GetItems();


        public string OptionLabel { get; set; }

        /// <summary>
        /// The maximum amount of items that will be displayed as Checkboxes or Radio buttons.
        /// If the number of items is greater than this number, the UI will render either a DropDownList or ListBox
        /// </summary>
        public int BulkSelectionThreshold { get; set; }


        public void OnMetadataCreated(ModelMetadata metadata)
        {
            bool allowMultipleSelection = typeof(System.Collections.IEnumerable).IsAssignableFrom(metadata.ModelType);

            var selectorModel = new Selector
            {
                OptionLabel = OptionLabel,
                BulkSelectionThreshold = BulkSelectionThreshold,
                AllowMultipleSelection = allowMultipleSelection,
                Items = GetItems().ToList(),
            };

            metadata.TemplateHint = "Selector";
            metadata.AdditionalValues["SelectorModelMetadata"] = selectorModel;
        }
    }
}