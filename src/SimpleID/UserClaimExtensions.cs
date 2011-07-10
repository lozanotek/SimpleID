namespace SimpleID {
    using System.Web.Script.Serialization;
    using System.Web.Security;

    public static class UserClaimExtensions {
        public static string ToJson(this UserClaim claim) {
            return new JavaScriptSerializer().Serialize(claim);
        }

        public static UserClaim FromFormsIdentity(this FormsIdentity formsIdentity) {
            if (formsIdentity == null) return null;
            if (formsIdentity.Ticket == null) return null;

            var userData = formsIdentity.Ticket.UserData;
            return string.IsNullOrWhiteSpace(userData) ? null : 
                new JavaScriptSerializer().Deserialize<UserClaim>(userData);
        }
    }
}