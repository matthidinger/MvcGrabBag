using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcGrabBag.Web.Helpers
{
    public class RadioButtonListViewModel
    {
        public static RadioButtonListViewModel<bool> FromBool(bool selectedValue, string tooltip)
        {
            var vm = new RadioButtonListViewModel<bool>
            {
                SelectedValue = selectedValue,
                ListItems =
                    new List<RadioButtonListItem<bool>>
                                     {
                                         new RadioButtonListItem<bool>
                                             {
                                                 Text = "Yes",
                                                 Value = true,
                                                 Selected = selectedValue,
                                                 Tooltip = tooltip
                                             },
                                         new RadioButtonListItem<bool>
                                             {
                                                 Text = "No",
                                                 Value = false,
                                                 Selected = selectedValue,
                                                 Tooltip = tooltip
                                             }
                                     }
            };


            return vm;
        }

        public static RadioButtonListViewModel<T> FromEnum<T>(T selectedValue)
        {
            var vm = new RadioButtonListViewModel<T>
            {
                SelectedValue = selectedValue,
                ListItems = new List<RadioButtonListItem<T>>()
            };

            foreach (var name in Enum.GetNames(typeof(T)))
            {
                var listItem = new RadioButtonListItem<T>
                {
                    Text = name.Wordify(),
                    Value = (T)Enum.Parse(typeof(T), name, true),
                };

                if (listItem.Value.Equals(selectedValue))
                    listItem.Selected = true;


                var displayAttribute = typeof(T).GetMember(name)[0].GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().FirstOrDefault();
                if (displayAttribute != null)
                    listItem.Tooltip = displayAttribute.Description;


                vm.ListItems.Add(listItem);
            }

            return vm;
        }
    }

    public class RadioButtonListViewModel<T> : RadioButtonListViewModel
    {
        private T _selectedValue;

        public T SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                UpdatedSelectedItems();
            }
        }

        private void UpdatedSelectedItems()
        {
            if (ListItems == null)
                return;

            ListItems.ForEach(li => li.Selected = Equals(li.Value, SelectedValue));
        }

        private List<RadioButtonListItem<T>> _listItems;

        public List<RadioButtonListItem<T>> ListItems
        {
            get { return _listItems; }
            set
            {
                _listItems = value;
                UpdatedSelectedItems();
            }
        }
    }
}