using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AvalancheGamesWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex is System.Threading.ThreadAbortException)
                return;//redirects may cause this exception
            Logging.Logger.Log(ex);
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string Email = Session["AUTHEmail"] as string;
            string Sessroles = Session["AUTHRoles"] as string;
            string ValidationType = Session["AUTHTYPE"] as string;
            if (string.IsNullOrEmpty(Email))
            {
                return;
            }
            GenericIdentity i = new GenericIdentity(Email, ValidationType);
            if (Sessroles == null) { Sessroles = ""; }
            string[] roles = Sessroles.Split(' ');
            GenericPrincipal p = new GenericPrincipal(i, roles);
            HttpContext.Current.User = p;
        }
    }
}
