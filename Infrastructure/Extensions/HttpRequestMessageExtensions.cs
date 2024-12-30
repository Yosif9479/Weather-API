using System.Collections.Specialized;
using System.Web;

namespace Infrastructure.Extensions;

public static class HttpRequestMessageExtensions
{
	public static HttpRequestMessage AddParameter(this HttpRequestMessage request, string key, string value, HttpClient? client = null)
	{
        ArgumentNullException.ThrowIfNull(request);
		ArgumentNullException.ThrowIfNull(request.RequestUri);

		if (client is not null)
		{
			request.RequestUri = new Uri(client.BaseAddress, request.RequestUri);
		}

		if (!request.RequestUri.IsAbsoluteUri) throw new ArgumentException("Uri is not absolute. Either provide absolute url or clien");
		
		var builder = new UriBuilder(request.RequestUri);
        
        NameValueCollection query = HttpUtility.ParseQueryString(request.RequestUri.Query);
        query[key] = value;
        
        builder.Query = query.ToString();
        
        request.RequestUri = builder.Uri;
        
        return request;
	}
}