using System.Data.Entity;

namespace MvcGrabBag.Web.Models
{
    public interface IDataContext
    {
        IDbSet<Category> Categories { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class FakeDataContext : IDataContext
    {
        public IDbSet<Category> Categories { get; set; }
    }
}