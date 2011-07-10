namespace SimpleID {
    using System;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
    using DotNetOpenAuth.OpenId.RelyingParty;
    using SimpleID.Config;

    public class RelyPartyService : IRelyPartyService {
        private static readonly object __lock = new object();
        public const string RedirectUrlKey = "redirectUrl";
        private static OpenIdRelyingParty relyParty;

        public virtual IUrlProvider UrlProvider { get; set; }
        
        public virtual OpenIdRelyingParty RelyParty {
            get {
                if(relyParty == null) {
                    lock(__lock) {
                        if(relyParty == null) {
                            relyParty = SimpleRuntime.Instance.RelyingParty();
                        }
                    }
                }

                return relyParty;
            }

            set { relyParty = value; }
        }
        
        public RelyPartyService() {
            UrlProvider = UrlProvider ?? SimpleRuntime.Instance.UrlProvider();
        }

        public virtual IAuthenticationResponse GetResponse() {
            return RelyParty.GetResponse();
        }

        public virtual UserClaim GetUserClaim(IAuthenticationResponse response) {
            if (response == null) return null;

            switch (response.Status) {
                case AuthenticationStatus.Authenticated:
                    var claimsResponse = response.GetExtension<ClaimsResponse>();

                    if (claimsResponse != null) {
                        return new UserClaim {
                            Username = claimsResponse.Nickname,
                            Email = claimsResponse.Email,
                            Name = claimsResponse.FullName,
                            Identifier = response.FriendlyIdentifierForDisplay
                        };
                    }

                    return null;
                case AuthenticationStatus.Canceled:
                case AuthenticationStatus.Failed:
                    return null;
            }

            return null;
        }

        public virtual IAuthenticationRequest CreateRequest(AuthRequest authRequest) {
            var returnToBuilder = new UriBuilder(authRequest.RequestUri) {                                                                 
                Query = "", 
                Fragment = "", 
                Path = UrlProvider.CallbackUrl
            };

            var returnTo = returnToBuilder.Uri;
            returnToBuilder.Path = "/";

            Realm realm = returnToBuilder.Uri;
            var request = RelyParty.CreateRequest(authRequest.ProviderUrl, realm, returnTo);

            // Get the common information for user
            var claimsRequest = new ClaimsRequest {
                Email = DemandLevel.Require,
                Nickname = DemandLevel.Require,
                FullName = DemandLevel.Require
            };

            request.AddExtension(claimsRequest);

            var returnUrl = CleanUpRedirectUrl(authRequest.RedirectUrl);

            if (returnUrl != null) {
                request.SetCallbackArgument(RedirectUrlKey, returnUrl);
            }

            return request;
        }

        public virtual string GetRedirectUrl() {
            var response = GetResponse();
            return response == null ? null : response.GetCallbackArgument(RedirectUrlKey);
        }

        protected virtual string CleanUpRedirectUrl(string redirectUrl) {
            // HACK: For some reason, OpenID on the first handshake appends a ',' on the Return URL parameter.
            // We need to remove this ',' in order to create a valid return URL to then process in our end
            // when we come back from authentication.
            if (redirectUrl != null) {
                var index = redirectUrl.IndexOf(",");
                if (index > 0) {
                    redirectUrl = redirectUrl.Substring(0, index);
                }
            }

            return redirectUrl;
        }
    }
}