using System.Web;
using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility;

namespace DotNet.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            TelemetryConfiguration.Active.InstrumentationKey = "";
        }
    }
}
