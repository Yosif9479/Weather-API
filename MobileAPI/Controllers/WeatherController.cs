using Domain.Dtos.Responses;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MobileAPI.Controllers;

[Route("api/[controller]")]
public class WeatherController(IWeatherService service) : Controller
{
	/// <summary>
	/// Get current weather state by city name
	/// </summary>
	[HttpGet("current/{city}")]
	public async Task<ActionResult<WeatherCurrentResponse>> GetCurrentWeather(string city)
	{
		return await service.GetCurrentWeatherAsync();
	}
}