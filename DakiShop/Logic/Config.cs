namespace DakiShop.Logic
{
	public static class Config
	{
		public static IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
	}
}
