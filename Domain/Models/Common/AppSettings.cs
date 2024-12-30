namespace Domain.Models.Common;

public class AppSettings
{
	public required string Host { get; init; }
	public int Port { get; init; }
	public required string ExternalApiKey { get; init; }
	public required string WeatherProviderUrl { get; init; }
}