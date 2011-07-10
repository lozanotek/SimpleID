namespace SimpleID {
    using System;

    [Serializable]
    public class AuthRequest {
        public string ProviderUrl { get; set; }
        public Uri RequestUri { get; set; }
        public string RedirectUrl { get; set; }
    }
}