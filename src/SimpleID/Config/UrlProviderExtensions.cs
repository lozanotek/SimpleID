namespace SimpleID.Config {
    using System;

    public static class UrlProviderExtensions {
        public static IUrlProvider UrlProvider(this SimpleRuntime runtime) {
            return runtime.FromCache<IUrlProvider>();
        }

        public static SimpleRuntime UrlProvider<TService>(this SimpleRuntime runtime) 
            where TService : class, IUrlProvider, new() {
            
            return UrlProvider(runtime, () => new TService());
        }

        public static SimpleRuntime UrlProvider(this SimpleRuntime runtime, Func<IUrlProvider> resolver) {
            if(resolver == null) {
                resolver = () => new UrlProvider();
            }

            runtime.ToCache(typeof(IUrlProvider), resolver);
            return runtime;
        }
    }
}