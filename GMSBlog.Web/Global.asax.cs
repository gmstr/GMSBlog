using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GMSBlog.Web.Support;
using System.Configuration;

namespace GMSBlog.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                        "RSS Feed",
                        "Feed",
                        new { controller = "Home", action = "Feed" }
                        );

            routes.MapRoute(
                "SEO Posts",                                              // Route name
                "{year}-{month}-{day}/{title}",                           // URL with parameters
                new { controller = "Home", action = "PostByName" }  // Parameter defaults
            );

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        private static string _blogTitle = "";
        public static string BlogTitle
        {
            get
            {
                return _blogTitle;
            }
        }

        private static string _blogSubtitle = "";
        public static string BlogSubtitle
        {
            get
            {
                return _blogSubtitle;
            }
        }


        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            Bootstrapper.ConfigureStructureMap();

            _blogTitle = ConfigurationManager.AppSettings["BlogTitle"];
            _blogSubtitle = ConfigurationManager.AppSettings["BlogSubtitle"];

        }
    }
}