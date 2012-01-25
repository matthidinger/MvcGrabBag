using System.Data.Entity;

namespace MvcGrabBag.Web.EntityFramework
{
    public class ECommerceDb : DbContext, IDataContext
    {
        public ECommerceDb()
        {
            // Automatically filter out Categories where IsDeleted == false
            Categories = new FilteredDbSet<Category>(this, c => c.IsDeleted == false);
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Product> Products { get; set; }
    }
}