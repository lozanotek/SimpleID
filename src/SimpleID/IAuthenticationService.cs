namespace SimpleID {
    public interface IAuthenticationService {
        void SetAuthenticationTicket(UserClaim userClaim);
        string GetDefaultRedirectUrl();
    }
}
