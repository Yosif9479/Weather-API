using Domain.Models.Common;
using Infrastructure.Extensions;

namespace Infrastructure.Clients;

/// <summary>
/// External Weather API Http Client
/// </summary>
public class ExtHttpClient : HttpClient
{
	private readonly string _apiKey;
	
	public ExtHttpClient(AppSettings settings)
	{
		BaseAddress = new Uri(settings.WeatherProviderUrl);
		_apiKey = settings.ExternalApiKey;
	}

	public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		SetCommonOptions(request);
		return base.Send(request, cancellationToken);
	}

	public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		SetCommonOptions(request);
		return base.SendAsync(request, cancellationToken);
	}

	private void SetCommonOptions(HttpRequestMessage request)
	{
		request.AddParameter("key", _apiKey, this);
	}
}