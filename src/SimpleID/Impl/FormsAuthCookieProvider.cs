namespace SimpleID {
    using System;
    using System.Web;
    using System.Web.Security;

    public static class FormsAuthCookieProvider
    {
        private static Action<UserClaim> currentProvider;

        static FormsAuthCookieProvider()
        {
            SetCookieProvider(CreateDefaultIdentityCookie);
        }

        public static void SetCookieProvider(Action<UserClaim> newProvider)
        {
            currentProvider = newProvider;
        }

        public static Action<UserClaim> CurrentProvider { get { return currentProvider; } }

        private static void CreateDefaultIdentityCookie(UserClaim userClaim)
        {
            var now = DateTime.Now;
            var claimJson = userClaim.ToJson();

            var ticket = new FormsAuthenticationTicket(1,
                                                       userClaim.Username,
                                                       now,
                                                       now.Add(FormsAuthentication.Timeout),
                                                       false,
                                                       claimJson,
                                                       FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                         {
                             Domain = FormsAuthentication.CookieDomain,
                             HttpOnly = true,
                             Path = FormsAuthentication.FormsCookiePath
                         };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}