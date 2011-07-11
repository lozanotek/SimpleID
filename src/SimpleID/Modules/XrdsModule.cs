namespace SimpleID.Modules {
    using System;
    using System.Web;
    using SimpleID.Config;

    public class XrdsModule : IHttpModule {
        public IUrlProvider UrlProvider { get; set; }

        public XrdsModule() {
            UrlProvider = UrlProvider ?? SimpleRuntime.Instance.UrlProvider();
        }

        public void Init(HttpApplication context) {
            context.PreSendRequestHeaders += PreSendRequestHeaders;
        }

        protected virtual void PreSendRequestHeaders(object sender, EventArgs e) {
            var app = sender as HttpApplication;
            var context = app.Context;
            var response = context.Response;
            var request = context.Request;

            var absoluteUrl = new Uri(request.Url, response.ApplyAppPathModifier(UrlProvider.XrdsUrl)).AbsoluteUri;
            response.AppendHeader("X-XRDS-Location", absoluteUrl);
        }

        public void Dispose() {
        }
    }
}