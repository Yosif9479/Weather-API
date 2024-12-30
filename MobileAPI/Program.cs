namespace MobileAPI;

internal static class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		
		builder.Configure();
		builder.Services.AddControllers();
		builder.Services.ConfigureSwagger();
		
		WebApplication app = builder.Build();

		app.MapControllers();

		app.UseSwagger();
		app.UseSwaggerUI();
		
		app.SetupAndRun();
	}
}