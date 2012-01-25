using System.Data.Entity;

namespace MvcGrabBag.Web.EntityFramework
{
    public interface IDataContext
    {
        IDbSet<Category> Categories { get; set; }
        IDbSet<Product> Products { get; set; }
    }
}