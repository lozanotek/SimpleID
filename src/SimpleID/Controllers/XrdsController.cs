namespace SimpleID.Controllers {
    using System.Web.Mvc;

    public class XrdsController : Controller {
        public UrlProvider UrlProvider { get; set; }

        public XrdsController(UrlProvider urlProvider) {
            UrlProvider = urlProvider;
        }

        public ActionResult Index() {
            return View(UrlProvider.CallbackUrl);
        }
    }
}