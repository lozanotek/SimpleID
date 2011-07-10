namespace SimpleID.Modules {
    using System;
    using System.Web;

    public class XrdsModule : IHttpModule {
        public UrlProvider UrlProvider { get; private set; }

        public XrdsModule(UrlProvider urlProvider) {
            UrlProvider = urlProvider;
        }

        public void Init(HttpApplication context) {
            context.PreSendRequestHeaders += PreSendRequestHeaders;
        }

        protected virtual void PreSendRequestHeaders(object sender, EventArgs e) {
            var app = sender as HttpApplication;
            var context = app.Context;
            var response = context.Response;
            var request = context.Request;

            var absoluteUrl = new Uri(request.Url, UrlProvider.XrdsUrl).AbsoluteUri;
            response.AppendHeader("X-XRDS-Location", absoluteUrl);
        }

        public void Dispose() {
        }
    }
}