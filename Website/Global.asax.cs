
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Poco.Entities;
using ServiceStack;

namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static JsvServiceClient ServiceClient { get; set; } //= new JsvServiceClient("http://localhost:63957/");

        static MvcApplication()
        {

            ServiceClient = new JsvServiceClient("http://localhost:10050");//{ UserName = "Maksym", Password = "Maks123$" };
          //  ServiceClient.AlwaysSendBasicAuthHeader = true;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
