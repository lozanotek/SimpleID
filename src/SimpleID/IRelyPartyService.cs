namespace SimpleID {
    using DotNetOpenAuth.OpenId.RelyingParty;

    public interface IRelyPartyService {
        IAuthenticationResponse GetResponse();
        UserClaim GetUserClaim(IAuthenticationResponse response);
        IAuthenticationRequest CreateRequest(AuthRequest request);
        string GetRedirectUrl();
    }
}
