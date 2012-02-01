using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcGrabBag.Web.EntityFramework;
using MvcGrabBag.Web.Helpers;
using Telerik.Web.Mvc;

namespace MvcGrabBag.Web.Controllers
{
    public class TelerikController : Controller
    {
        private readonly IDataContext _db;

        public TelerikController(IDataContext db)
        {
            _db = db;
        }

        [GridAction(EnableCustomBinding = true)]
        public ActionResult Index(GridCommand command)
        {
            var query = _db.Products;
            int rowCount;

            var grid2 = query.OrderBy(m => m.Id)
                .ForGrid(new DashboardModel(), command, out rowCount);
            

            var grid = query.OrderBy(m => m.Id)
                .ForGrid(serverModel =>
                         new
                             {
                                 serverModel.Id,
                                 ProductName = serverModel.Name,
                                 CategoryName = serverModel.Category.Name,
                                 serverModel.DateCreated
                             },
                         clientModel =>
                         new ProductDashboardModel
                             {
                                 Id = clientModel.Id,
                                 ProductName = clientModel.ProductName,
                                 CategoryName = clientModel.CategoryName,
                                 DaysOld = DateTime.Now.Subtract(clientModel.DateCreated.GetValueOrDefault()).Days,
                             },
                         command, new ClaimsDashboardGridPropertyMap(), out rowCount);

            ViewBag.Total = rowCount;
            return View(grid);
        }
    }

    public class ProductDashboardModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public int DaysOld { get; set; }
    }

    public class GridProperty<TEntity, TProp> : IGridProperty
    {
        public GridPropertyValue<TValue, TProp> ServerValue<TValue>(Expression<Func<TEntity, TValue>> selector)
        {
            return new GridPropertyValue<TValue, TProp>();
        }
    }

    public interface IGridProperty
    {
    }

    public class GridPropertyValue<TValue, TExpected>
    {
        public void Returns(Func<TValue, TExpected> selector)
        {

        }
    }

    public class GridModel<TEntity, TModel> : IGridMap
    {
        private readonly List<IGridProperty> _properties = new List<IGridProperty>();

        public GridProperty<TEntity, TProp> Property<TProp>(Expression<Func<TModel, TProp>> selector)
        {
            var prop = new GridProperty<TEntity, TProp>();
            _properties.Add(prop);
            return prop;
        }
    }

    public interface IGridMap
    {

    }

    public class DashboardModel : GridModel<Product, ProductDashboardModel>
    {
        public DashboardModel()
        {
            ServerQuery(m => new
                                 {
                                     m.Id,
                                     m.DateCreated.GetValueOrDefault()
                                 });

            ClientModel()

            Property(m => m.Id).ServerValue(m => m.Id);

            Property(m => m.DaysOld)
                .ServerValue(m => m.DateCreated.GetValueOrDefault())
                .Returns(m => DateTime.Now.Subtract(m).Days);


        }

    }

    public class ClaimsDashboardGridPropertyMap : IGridPropertyMap
    {
        public string GetServerSidePropertyName(string clientProperty)
        {
            switch (clientProperty)
            {
                case "DaysOld":
                    return "DateCreated";

                case "CategoryName":
                    return "Category.Name";

                case "ProductName":
                    return "Name";

                default:
                    return clientProperty;
            }
        }
    }
}