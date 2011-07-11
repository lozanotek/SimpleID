namespace SimpleID.Controllers {
    using System;
    using System.Web.Mvc;
    using SimpleID.Config;

    public class XrdsController : Controller {
        public IUrlProvider UrlProvider { get; set; }

        public XrdsController() {
            UrlProvider = UrlProvider ?? SimpleRuntime.Instance.UrlProvider();
        }

        public ActionResult Index() {
            var callbackUrl = UrlProvider.CallbackUrl;
            var xrdsUri = new Uri(Request.Url, Response.ApplyAppPathModifier(callbackUrl));
            return View("xrds", xrdsUri);
        }
    }
}