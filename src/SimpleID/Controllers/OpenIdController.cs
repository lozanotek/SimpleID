namespace SimpleID.Controllers {
    using System;
    using System.Web.Mvc;
    using DotNetOpenAuth.Messaging;
    using SimpleID.Config;

    public class OpenIdController : Controller {
        public IRelyPartyService RelyService { get; set; }
        public IAuthenticationService AuthenticationService { get; set; }
        public IUrlProvider UrlProvider { get; set; }

        public OpenIdController() {
            RelyService = RelyService ?? SimpleRuntime.Instance.RelyPartyService();
            AuthenticationService = AuthenticationService ?? SimpleRuntime.Instance.AuthenticationService();
            UrlProvider = UrlProvider ?? SimpleRuntime.Instance.UrlProvider();
        }

        public  virtual ActionResult Index() { 
            return View();
        }

        public ActionResult Xrds() {
            var callbackUrl = UrlProvider.CallbackUrl;
            var xrdsUri = new Uri(Request.Url, Response.ApplyAppPathModifier(callbackUrl));
            return View("xrds", xrdsUri);
        }

        public virtual ActionResult ReturnTo() {
            var response = RelyService.GetResponse();
            var userClaim = RelyService.GetUserClaim(response);

            if (userClaim == null) {
                var invalidClaim = new InvalidClaim {Identifier = response.FriendlyIdentifierForDisplay};
                return View("InvalidResponse", invalidClaim);
            }

            AuthenticationService.SetAuthenticationTicket(userClaim);
            var redirectUrl = RelyService.GetRedirectUrl();
            var defaultUrl = AuthenticationService.GetDefaultRedirectUrl();

            return Redirect(redirectUrl ?? defaultUrl);
        }

        [ValidateInput(false)]
        public virtual ActionResult Authenticate(string openid_identifier, string returnUrl) {
            var response = RelyService.GetResponse();
            if (response != null) return new EmptyResult();

            var authRequest = new AuthRequest {
                ProviderUrl = openid_identifier, 
                RedirectUrl = returnUrl, 
                RequestUri = Request.Url
            };

            var request = RelyService.CreateRequest(authRequest);
            return request.RedirectingResponse.AsActionResult();
        }
    }
}
