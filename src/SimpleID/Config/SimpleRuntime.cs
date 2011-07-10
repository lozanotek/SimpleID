namespace SimpleID.Config {
    using System;
    using System.Collections.Generic;
    using DotNetOpenAuth.OpenId.RelyingParty;

    public class SimpleRuntime {
        private static readonly SimpleRuntime instance = new SimpleRuntime();
        private static readonly IDictionary<Type, Func<object>> typeCache = new Dictionary<Type, Func<object>>();

        private SimpleRuntime() {
            this
            .RelyPartyService<RelyPartyService>()
            .AuthenticationService<AuthenticationService>()
            .UrlProvider<UrlProvider>()
            .RelyingParty<OpenIdRelyingParty>();
        }

        public static SimpleRuntime Instance {
            get { return instance; }
        }
    
        internal void ToCache<TService>(Type key, Func<TService> resolver) 
            where TService :class {
            typeCache[key] = resolver;
        }

        internal TService FromCache<TService>() where TService : class {
            return FromCache<TService>(typeof (TService));
        }

        internal TService FromCache<TService>(Type key) where TService : class {
            if (!typeCache.ContainsKey(key)) return default(TService);
            
            var resolver = typeCache[key];
            return resolver() as TService;
        }
    }
}
