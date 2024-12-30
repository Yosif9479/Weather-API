using System.Runtime.InteropServices;
using Domain.Dtos.Responses;
using Domain.Services;
using Infrastructure.Clients;
using Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Services;

public class WeatherService(ExtHttpClient httpClient) : IWeatherService
{
	private const string CurrentWeatherRoute = "current.json";
	
	private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
	{
		ContractResolver = new DefaultContractResolver
		{
			NamingStrategy = new SnakeCaseNamingStrategy()
		},
		Converters = new List<JsonConverter> { new StringEnumConverter() }
	};
	
	public async Task<WeatherCurrentResponse> GetCurrentWeatherAsync(string cityName)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, CurrentWeatherRoute);
		
		request.AddParameter("q", cityName, httpClient);
		
		HttpResponseMessage externalResponse = await httpClient.SendAsync(request, new CancellationToken());
		
		if (!externalResponse.IsSuccessStatusCode) throw new ExternalException($"Unable to get current weather from external API");
		
		string content = await externalResponse.Content.ReadAsStringAsync();

		if (string.IsNullOrWhiteSpace(content)) throw new ExternalException($"External API returned an empty content");
		
		JObject jsonObject = JObject.Parse(content);
		
		content = jsonObject["current"]?.ToString() ?? string.Empty;

		var current = JsonConvert.DeserializeObject<Domain.Dtos.ExternalResponses.WeatherCurrentResponse>(content, _serializerSettings);
		
		return new WeatherCurrentResponse {
			Temperature = current.TempC,
			Precipitation = current.PrecipIn,
			Humidity = current.Humidity,
			StateText = current.Condition.Text,
			IconUrl = current.Condition.Icon,
			DayOfWeek = current.IsDay,
			Wind = current.WindKph
		};
	}
}