namespace Domain.Dtos.Responses;

public class WeatherCurrentResponse
{
	public float Temperature { get; set; }
	public float Humidity { get; set; }
	public float Precipitation { get; set; }
	public float Wind { get; set; }
	public DayOfWeek DayOfWeek { get; set; }
	public required string StateText { get; set; }
	public required string IconUrl { get; set; }
}