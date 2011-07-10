namespace SimpleID {
    public interface IUrlProvider {
        string CallbackUrl { get; }
        string XrdsUrl { get; }
    }
}
