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