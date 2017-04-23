using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaySystem.Client
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
            name: "StatisBill",
            url: "{controller}/{action}/{billId}",
            defaults: new { controller = "Bill", action = "DetailsBill", billId = "" }
            );

            routes.MapRoute(
            name: "GetMoney",
            url: "{controller}/{action}/{pictureName}/{leva}",
            defaults: new { controller = "Bill", action = "GetMoney", pictureName = "", leva = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
