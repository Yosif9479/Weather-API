using Domain.Dtos.Responses;
using Domain.Services;

namespace Infrastructure.Services;

public class WeatherService : IWeatherService
{
	public async Task<WeatherCurrentResponse> GetCurrentWeatherAsync()
	{
		//TODO: Properly implement this logic
		return new WeatherCurrentResponse();
	}
}