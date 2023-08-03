namespace AuthAPI.Engine.Settings
{
    public class Appsettings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }
}
