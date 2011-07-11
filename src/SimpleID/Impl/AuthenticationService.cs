namespace SimpleID {
    using System;

    public class AuthenticationService : IAuthenticationService {
        public void SetAuthenticationTicket(UserClaim userClaim) {
            var provider = FormsAuthCookieProvider.CurrentProvider;
            if (provider == null) {
                throw new InvalidOperationException("FormsAuthentication Cookie Provider is null.");
            }

            provider(userClaim);
        }

        public string GetDefaultRedirectUrl() {
            return "/";
        }
    }
}