namespace BookStory.Common
{
    public static class ConnectionSettings
    {
        private const string DatabaseAddress = "87.252.185.32";
        private const string DatabasePort = "1433";
        private const string DatabaseUsername = "admin";
        private const string DatabasePassword = "BojanchO_88";

        public const string LocalDbConnectionString = "Server=DESKTOP-QHTHL23\\SQLEXPRESS01;Database=BookStory;Trusted_Connection=True;TrustServerCertificate=True";
        public const string RemoteDbConnectionString = $"Server={DatabaseAddress}, {DatabasePort};Database=BookStory;User Id={DatabaseUsername};Password={DatabasePassword};TrustServerCertificate=true;";

        public static string Settings()
        {
            return null;
        }
    }
}
