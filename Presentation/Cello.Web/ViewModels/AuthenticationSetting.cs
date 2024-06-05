namespace Cello.Web.ViewModels
{
    public class AuthenticationSetting
    {
        public string Authority { get; set; } = null!;
        public string? ClientId { get; set; }
        public string? RedirectUri { get; set; }
        public string? ResponseType { get; set; }
        public string? ResponseMode { get; set; }
        public string? Scope { get; set; }
    }
}
