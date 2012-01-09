using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MvcGoodies.Web.Helpers
{
    public class RadioButtonListViewModel<T>
    {
        public static RadioButtonListViewModel<bool> FromBool(string id, bool selectedValue, string tooltip)
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

        public static RadioButtonListViewModel<T> FromEnum(string id, T selectedValue)
        {
            var vm = new RadioButtonListViewModel<T>
                         {
                             SelectedValue = selectedValue,
                             ListItems = new List<RadioButtonListItem<T>>()
                         };

            foreach (var name in Enum.GetNames(typeof (T)))
            {
                var listItem = new RadioButtonListItem<T>
                                   {
                                       Text = name.Wordify(),
                                       Value = (T) Enum.Parse(typeof (T), name, true),
                                   };

                if (listItem.Value.Equals(selectedValue))
                    listItem.Selected = true;


                var type = typeof (T);
                var memInfo = type.GetMember(name);
                var attribute =
                    memInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false).OfType<DescriptionAttribute>().
                        FirstOrDefault();

                if (attribute != null)
                    listItem.Tooltip = attribute.Description;


                vm.ListItems.Add(listItem);
            }

            return vm;
        }

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