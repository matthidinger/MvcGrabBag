using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using MvcGrabBag.Web.EntityFramework;
using MvcGrabBag.Web.FileUpload;
using MvcGrabBag.Web.Metadata;
using MvcGrabBag.Web.Models;

namespace MvcGrabBag.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Caching",
                "Caching",
                new {controller = "Caching", Action = "Index"}
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //ModelValidatorProviders.Providers.Add(new UploadedFileModelValidatorProvider());

            var customMetadataProvider = new CustomMetadataProvider();
            ModelMetadataProviders.Current = customMetadataProvider;


            var container = new UnityContainer();
            container.RegisterType<IDataContext, ECommerceDb>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            Database.SetInitializer(new ECommerceDbInitializer());
        }
    }
}