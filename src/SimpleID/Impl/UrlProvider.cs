namespace SimpleID {
    public class UrlProvider : IUrlProvider {
        public virtual string CallbackUrl {
            get {
                return "~/openid/returnto";
            }
        }

        public virtual string XrdsUrl {
            get {
                return "~/xrds/index";
            }
        }
    }
}