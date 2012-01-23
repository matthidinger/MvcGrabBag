using System.Collections.Generic;
using System.Web.Mvc;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.Selectors
{
    public class CategorySelectorAttribute : SelectorAttribute
    {
        public CategorySelectorAttribute()
        {
            // The Category selector should always be rendered as a drop down
            BulkSelectionThreshold = 0;
        }

        public override IEnumerable<SelectListItem> GetItems()
        {
            // You could of course get these values from a database, similar to:
            //var dataContext = DependencyResolver.Current.GetService<IDataContext>();

            var categories = new List<Category>
                                 {
                                     new Category {Id = 1, Name = "Beverages"},
                                     new Category {Id = 2, Name = "Tools"},
                                     new Category {Id = 3, Name = "Soup"},
                                 };
            return Selector.GetItems(categories, m => m.Id, m => m.Name);
        }
    }
}