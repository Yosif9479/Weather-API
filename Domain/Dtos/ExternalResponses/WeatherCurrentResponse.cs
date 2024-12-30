namespace Domain.Dtos.ExternalResponses;

public class WeatherCurrentResponse
{
	public float TempC { get; set; }
	public WeatherConditionDto Condition { get; set; } = new();
	public float WindKph { get; set; }
	public float PrecipIn { get; set; }
	public DayOfWeek IsDay { get; set; }
}