namespace BlobAPI.Engine.Settings
{
    public class Appsettings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
        public Tokensettings? Tokensettings { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }

    public class Tokensettings
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretKey { get; set; }
    }
}
