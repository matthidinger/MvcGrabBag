using System.Collections.Generic;

namespace MvcGrabBag.Web.EntityFramework
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();    
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; private set; }
    }
}