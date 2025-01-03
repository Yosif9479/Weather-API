﻿using Domain.Services;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Domain.Models.Common;
using Infrastructure.Clients;

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
		app.Services.AddScoped<ExtHttpClient>();
		app.Services.AddSingleton(ReadAppSettings(app.Configuration));
		app.Services.AddScoped<IWeatherService, WeatherService>();
		return app.Services;
	}

	public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Mobile API", Version = "v1" });
		});
		
		return services;
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