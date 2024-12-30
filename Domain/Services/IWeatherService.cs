using Domain.Dtos.Responses;

namespace Domain.Services;

public interface IWeatherService
{
	public Task<WeatherCurrentResponse> GetCurrentWeatherAsync(string cityName);
}