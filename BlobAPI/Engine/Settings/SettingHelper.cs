namespace BlobAPI.Engine.Settings
{
    public static class SettingHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration?.GetSection("Appsettings:ConnectionStrings")["DefaultConnection"];
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException($"A DefaultConnection environment variable is missing.", nameof(connectionString));

            return connectionString;
        }
    }
}
