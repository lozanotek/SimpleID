namespace SimpleID.Controllers {
    using System.Web.Mvc;
    using DotNetOpenAuth.Messaging;
    using SimpleID.Config;

    public class OpenIdController : Controller {
        public IRelyPartyService RelyService { get; set; }
        public IAuthenticationService AuthenticationService { get; set; }

        public OpenIdController() {
            RelyService = RelyService ?? SimpleRuntime.Instance.RelyPartyService();
            AuthenticationService = AuthenticationService ?? SimpleRuntime.Instance.AuthenticationService();
        }

        public virtual ActionResult ReturnTo() {
            var response = RelyService.GetResponse();
            var userClaim = RelyService.GetUserClaim(response);

            if (userClaim == null) return new EmptyResult();

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
