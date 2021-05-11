
namespace TeamManagement.BusinessLayer.Options
{
    public class AuthOptions
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string ReturnURL { get; set; }
        public string ProviderName { get; set; }

        public string[] WhiteListedDomains { get; set; }
    }
}
