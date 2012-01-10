using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcGrabBag.Web.Helpers;

namespace MvcGrabBag.Web.Models
{
    public class Selector
    {
        public static IEnumerable<SelectListItem> GetItemsFromEnum<T>(T selectedValue = default(T)) where T : struct
        {
            return from name in Enum.GetNames(typeof(T))
                   let enumValue = Convert.ToString((T)Enum.Parse(typeof(T), name, true))
                   select new SelectListItem
                              {
                                  Text = name.Wordify(),
                                  Value = enumValue,
                                  Selected = enumValue.Equals(selectedValue)
                              };
        }

        public IEnumerable<SelectListItem> Items { get; set; }

        public string OptionLabel { get; set; }

        public bool AllowMultipleSelection { get; set; }

        public int RadioButtonThreshold { get; set; }

        public void PopulateItems(IEnumerable data)
        {
            Items = AllowMultipleSelection
                        ? new MultiSelectList(data)
                        : new SelectList(data);
        }

        public void PopulateItems(IEnumerable data, string dataValueField, string dataTextField)
        {
            Items = AllowMultipleSelection
                        ? new MultiSelectList(data, dataValueField, dataTextField)
                        : new SelectList(data, dataValueField, dataTextField);
        }

        public void PopulateItems<T>(IEnumerable<T> data, Expression<Func<T, object>> dataValueFieldSelector, Expression<Func<T, string>> dataTextFieldSelector)
        {
            var dataValueField = dataValueFieldSelector.ToPropertyInfo().Name;
            var dataTextField = dataTextFieldSelector.ToPropertyInfo().Name;
            PopulateItems(data, dataValueField, dataTextField);
        }
    }
}