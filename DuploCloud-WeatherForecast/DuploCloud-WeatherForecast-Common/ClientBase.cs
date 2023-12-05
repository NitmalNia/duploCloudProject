
namespace DuploCloud_WeatherForecast_Common;

/// <summary>
/// This class automatically sets the header 'Content-Type" and performs all of the api calls
/// </summary>
public class ClientBase
{
    public HttpClient Client { get { return _httpClient; } }
    private readonly HttpClient _httpClient;

    public ClientBase()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("dcwf-dotnet");
    }
}
