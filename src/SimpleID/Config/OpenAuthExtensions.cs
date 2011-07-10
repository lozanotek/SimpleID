namespace SimpleID.Config {
    using System;
    using DotNetOpenAuth.OpenId.RelyingParty;

    public static class OpenAuthExtensions {
        public static OpenIdRelyingParty RelyingParty(this SimpleRuntime runtime) {
            return runtime.FromCache<OpenIdRelyingParty>();
        }

        public static SimpleRuntime RelyingParty<TService>(this SimpleRuntime runtime)
            where TService : OpenIdRelyingParty, new() {
            return RelyingParty(runtime, () => new TService());
        }

        public static  SimpleRuntime RelyingParty(this SimpleRuntime runtime, Func<OpenIdRelyingParty> resolver) {
            if(resolver == null) {
                resolver = () => new OpenIdRelyingParty();
            }

            runtime.ToCache(typeof(OpenIdRelyingParty), resolver);
            return runtime;
        }
    }
}