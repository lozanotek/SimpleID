namespace SimpleID.Config {
    using System;

    public static class ReplyPartyExtensions {
        public static IRelyPartyService RelyPartyService(this SimpleRuntime runtime) {
            return runtime.FromCache<IRelyPartyService>();
        }

        public static SimpleRuntime RelyPartyService<TService>(this SimpleRuntime runtime) 
            where TService : class, IRelyPartyService, new() {
            
            return RelyPartyService(runtime, () => new TService());
        }

        public static SimpleRuntime RelyPartyService(this SimpleRuntime runtime, Func<IRelyPartyService> resolver) {
            if (resolver == null) {
                resolver = () => new RelyPartyService();
            }

            runtime.ToCache(typeof (IRelyPartyService), resolver);
            return runtime;
        }
    }
}