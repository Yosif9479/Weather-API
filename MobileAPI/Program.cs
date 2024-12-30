namespace MobileAPI;

internal static class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Configure();
		
		WebApplication app = builder.Build();

		app.SetupAndRun();
	}
}