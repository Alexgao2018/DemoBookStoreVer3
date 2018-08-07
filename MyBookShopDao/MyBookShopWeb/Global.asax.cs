using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MyBookShopWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    HttpApplication app = sender as HttpApplication;
        //    //通过了身份验证
        //    if (app.Context.Request.IsAuthenticated)
        //    {
        //        //获取用户数据（角色信息）
        //        string data = ((FormsIdentity)app.Context.User.Identity).Ticket.UserData;
        //        string[] roles = data.Split(',');
        //        FormsIdentity identity = app.Context.User.Identity as FormsIdentity;
        //        app.Context.User = new System.Security.Principal.GenericPrincipal(identity, roles);

        //    }
        //}
    }
}