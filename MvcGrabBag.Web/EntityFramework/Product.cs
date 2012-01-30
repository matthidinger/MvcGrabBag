namespace MvcGrabBag.Web.EntityFramework
{
    public class Product
    {
        public int Id { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}