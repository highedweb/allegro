﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;


namespace Allegro
{

    public class Global : HttpApplication
    {

        internal static string APP_NAME;
        internal static string BASE_URL;
        internal static string MACH_PATH;
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Proxy", // Route name
               "proxy/", // URL with parameters
               new { controller = "Allegro", action = "Proxy" } // Parameter defaults
           );

            routes.MapRoute(
               "Put map", // Route name
               "putMap/", // URL with parameters
               new { controller = "Allegro", action = "PutMap" } // Parameter defaults
           );

            routes.MapRoute(
               "Get Map", // Route name
               "getMap/{id}", // URL with parameters
               new { controller = "Allegro", action = "GetMap" } // Parameter defaults
           );
        }

        protected void Application_Start()
        {

          //put db in memory?

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            var mach = Environment.MachineName;

            switch (mach)
            {
                case "DRCWA":
                case "DRC10":
                case "DRC09":
                    MACH_PATH = "gis.oregonmetro.gov";
                    break;
                case "DRC05":
                    MACH_PATH = "qagis";
                    break;
                case "DRC06":
                    MACH_PATH = "devgis";
                    break;
                default:
                    MACH_PATH = "localhost";
                    break;
            }

            APP_NAME = ConfigurationManager.AppSettings["APP_NAME"]+"/";
            
            BASE_URL= "//" + MACH_PATH + "/" + APP_NAME;
        }
    }
}