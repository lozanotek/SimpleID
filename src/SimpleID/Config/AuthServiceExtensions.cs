namespace SimpleID.Config {
    using System;

    public static class AuthServiceExtensions {
        public static IAuthenticationService AuthenticationService(this SimpleRuntime runtime) {
            return runtime.FromCache<IAuthenticationService>();
        }

        public static SimpleRuntime AuthenticationService<TService>(this SimpleRuntime runtime) 
            where TService : class, IAuthenticationService, new() {

            return AuthenticationService(runtime, () => new TService());
        }

        public static SimpleRuntime AuthenticationService(this SimpleRuntime runtime, Func<IAuthenticationService> resolver) {
            if (resolver == null) {
                resolver = () => new AuthenticationService();
            }

            runtime.ToCache(typeof (IAuthenticationService), resolver);
            return runtime;
        }
    }
}