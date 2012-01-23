using System.Collections.Generic;

namespace MvcGrabBag.Web.Models
{
    public class Category
    {
        public Category()
        {
            SubCategories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string seName { get; set; }
        public int ParentId { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        //public ICollection<Product> Products { get; set; }
    }
}