using System.Collections.Generic;
using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Selectors
{
    public class DisplayModeSelectorAttribute : SelectorAttribute
    {
        public override IEnumerable<SelectListItem> GetItems()
        {
            return Selector.GetItemsFromEnum<DisplayMode>();
        }
    }
}