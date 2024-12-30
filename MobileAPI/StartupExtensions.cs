using Domain.Services;
using Infrastructure.Services;
using MobileAPI.Models.Common;

namespace MobileAPI;

internal static class StartupExtensions
{
	public static void SetupAndRun(this WebApplication app)
	{
		AppSettings settings = ReadAppSettings(app.Configuration);

		string host = settings.Host;
		int port = settings.Port;

		if (string.IsNullOrWhiteSpace(host)) host = "http://localhost";
		if (port is 0) port = 5000;
		
		app.Run($"{host}:{port}");
	}
	
	public static IServiceCollection Configure(this WebApplicationBuilder app)
	{
		app.Services.AddSingleton(ReadAppSettings(app.Configuration));
		app.Services.AddScoped<IWeatherService, WeatherService>();
		return app.Services;
	}

	private static AppSettings ReadAppSettings(IConfiguration configuration)
	{
		string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;
		
		string fileName = environment == Environments.Development ? "appsettings.Development.json" : "appsettings.json";

		if (!File.Exists(fileName)) throw new FileNotFoundException($"{fileName} is required");
		
		var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
		
		if (appSettings is null) throw new InvalidDataException($"Deserialized {fileName} is invalid");
		
		return appSettings;
	}
}