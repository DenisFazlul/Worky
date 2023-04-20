namespace Worky
{
    public static class AdminData
    {
        public static string AdminEmail { get; private set; }
        public static string AdminPassword { get; private set; }
        public static void Set(ConfigurationManager Configuration)
        {
           AdminEmail= Configuration.GetConnectionString("adminEmail");
           AdminPassword= Configuration.GetConnectionString("adminPass");
        }
    }
}
