using System.Data.Entity;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web.EntityFramework
{
    public class ECommerceDbInitializer : DropCreateDatabaseIfModelChanges<ECommerceDb>
    {
        protected override void Seed(ECommerceDb context)
        {
            context.Categories.Add(new Category { Name = "Beverages" });
            context.Categories.Add(new Category { Name = "Candy" });
            context.Categories.Add(new Category { Name = "Old Shoes", IsDeleted = true });
        }
    }
}