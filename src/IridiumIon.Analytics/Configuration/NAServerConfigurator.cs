namespace IridiumIon.Analytics.Configuration
{
    public static class NAServerConfigurator
    {
        internal static NAServerContext CreateContext(NAServerParameters serverParameters)
        {
            // load the configuration
            var context = new NAServerContext(serverParameters)
            {
            };
            return context;
        }
    }
}