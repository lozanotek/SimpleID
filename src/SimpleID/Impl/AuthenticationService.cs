namespace SimpleID {
    using System.Web.Security;

    public class AuthenticationService : IAuthenticationService {
        public void SetAuthenticationTicket(UserClaim userClaim) {
            FormsAuthentication.SetAuthCookie(userClaim.Username, false);
        }

        public string GetDefaultRedirectUrl() {
            return FormsAuthentication.DefaultUrl;
        }
    }
}