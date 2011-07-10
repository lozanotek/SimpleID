namespace SimpleID {
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class UrlProvider : IUrlProvider {
        private readonly HttpContextWrapper httpContext;

        public UrlProvider() {
            httpContext = new HttpContextWrapper(HttpContext.Current);
        }

        public virtual string CallbackUrl {
            get {
                var routeData = RouteTable.Routes.GetRouteData(httpContext);
                if (routeData == null) return "openid/return_to";

                var helper = new UrlHelper(new RequestContext(httpContext, routeData));
                return helper.RouteUrl("openIdCallback");
            }
        }

        public virtual string XrdsUrl {
            get {
                var routeData = RouteTable.Routes.GetRouteData(httpContext);
                if (routeData == null) return "openid/xrds";

                var helper = new UrlHelper(new RequestContext(httpContext, routeData));
                return helper.RouteUrl("openIdXRDS");
            }
        }
    }
}