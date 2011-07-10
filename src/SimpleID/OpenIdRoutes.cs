namespace Barcd.Server.OpenID {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class OpenIdRoutes : IRouteRegistrator {
        public void Register(RouteCollection routes) {
            routes.MapRoute("openIdCallback", "openid/returnto", new {controller = "openid", action = "returnto"});
            routes.MapRoute("openIdXRDS", "openid/xrds", new { controller = "xrds", action = "index" });
        }
    }
}