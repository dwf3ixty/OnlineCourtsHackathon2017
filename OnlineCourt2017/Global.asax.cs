using Autofac;
using Autofac.Integration.Mvc;
using Newtonsoft.Json;
using OnlineCourt2017.Data;
using OnlineCourt2017.Data.Interfaces;
using OnlineCourt2017.Data.Models;
using OnlineCourt2017.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace OnlineCourt2017
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly AutofacModule standardAutofacModule = new AutofacModule();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            builder.Register(o => new UserIdentity()).As<IUserIdentity>().InstancePerRequest();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(standardAutofacModule);
            builder.RegisterModule<AutofacWebTypesModule>();

            var autofacContainer = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(autofacContainer));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = JsonConvert.DeserializeObject<User>(authTicket.UserData);

                var newUser = new UserIdentity();
                newUser.SetIdentity(authTicket.Name);
                newUser.UserName = serializeModel.UserName;
                newUser.Name = serializeModel.Name;

                HttpContext.Current.User = newUser;
            }
        }
    }
}
