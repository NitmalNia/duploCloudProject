using DuploCloud_WeatherForecast.OptionsManagers;
using DuploCloud_WeatherForecast_Common;
using DuploCloud_WeatherForecast_Data.DataModel;
using System.Globalization;
using System.Text.Json;

namespace DuploCloud_WeatherForecast;

public class OpenMeteoClient
{
    private readonly string _weatherApiUrl = "https://api.open-meteo.com/v1/forecast";
    private readonly ClientBase clientBase;

    public OpenMeteoClient()
    {
        clientBase = new ClientBase();
    }


    public async Task<WeatherForecast?> QueryAsync(float latitude, float longitude)
    {
        WeatherForecastOptionsManager options = new WeatherForecastOptionsManager
        {
            Latitude = latitude,
            Longitude = longitude,

        };
        return await QueryAsync(options);
    }

    public async Task<WeatherForecast?> QueryAsync(WeatherForecastOptionsManager options)
    {
        try
        {
            return await GetWeatherForecastAsync(options);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task<WeatherForecast?> GetWeatherForecastAsync(WeatherForecastOptionsManager options)
    {
        try
        {
            HttpResponseMessage response = await clientBase.Client.GetAsync(MergeUrlWithOptions(_weatherApiUrl, options));
            response.EnsureSuccessStatusCode();

            WeatherForecast? weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return weatherForecast;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            return null;
        }

    }
    public string WeathercodeToString(int weathercode)
    {
        switch (weathercode)
        {
            case 0:
                return "Clear sky";
            case 1:
                return "Mainly clear";
            case 2:
                return "Partly cloudy";
            case 3:
                return "Overcast";
            case 45:
                return "Fog";
            case 48:
                return "Depositing rime Fog";
            case 51:
                return "Light drizzle";
            case 53:
                return "Moderate drizzle";
            case 55:
                return "Dense drizzle";
            case 56:
                return "Light freezing drizzle";
            case 57:
                return "Dense freezing drizzle";
            case 61:
                return "Slight rain";
            case 63:
                return "Moderate rain";
            case 65:
                return "Heavy rain";
            case 66:
                return "Light freezing rain";
            case 67:
                return "Heavy freezing rain";
            case 71:
                return "Slight snow fall";
            case 73:
                return "Moderate snow fall";
            case 75:
                return "Heavy snow fall";
            case 77:
                return "Snow grains";
            case 80:
                return "Slight rain showers";
            case 81:
                return "Moderate rain showers";
            case 82:
                return "Violent rain showers";
            case 85:
                return "Slight snow showers";
            case 86:
                return "Heavy snow showers";
            case 95:
                return "Thunderstorm";
            case 96:
                return "Thunderstorm with light hail";
            case 99:
                return "Thunderstorm with heavy hail";
            default:
                return "Invalid weathercode";
        }
    }
    private string MergeUrlWithOptions(string url, WeatherForecastOptionsManager? options)
    {
        if (options == null) return url;

        UriBuilder uri = new UriBuilder(url);
        bool isFirstParam = false;

        // If no query given, add '?' to start the query string
        if (uri.Query == string.Empty)
        {
            uri.Query = "?";

            // isFirstParam becomes true because the query string is new
            isFirstParam = true;
        }

        // Add the properties

        // Begin with Latitude and Longitude since they're required
        if (isFirstParam)
            uri.Query += "latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);
        else
            uri.Query += "&latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);

        uri.Query += "&longitude=" + options.Longitude.ToString(CultureInfo.InvariantCulture);

        uri.Query += "&temperature_unit=" + options.Temperature_Unit.ToString();
        uri.Query += "&windspeed_unit=" + options.Windspeed_Unit.ToString();
        uri.Query += "&precipitation_unit=" + options.Precipitation_Unit.ToString();
        if (options.Timezone != string.Empty)
            uri.Query += "&timezone=" + options.Timezone;

        uri.Query += "&timeformat=" + options.Timeformat.ToString();

        if (options.Start_date != string.Empty)
            uri.Query += "&start_date=" + options.Start_date;
        if (options.End_date != string.Empty)
            uri.Query += "&end_date=" + options.End_date;

        // Now we iterate through hourly and daily

        // Hourly
        if (options.Hourly != null && options.Hourly.Count > 0)
        {
            bool firstHourlyElement = true;
            uri.Query += "&hourly=";

            foreach (var option in options.Hourly)
            {
                if (firstHourlyElement)
                {
                    uri.Query += option.ToString();
                    firstHourlyElement = false;
                }
                else
                {
                    uri.Query += "," + option.ToString();
                }
            }
        }

        // Daily
        if (options.Daily != null && options.Daily.Count > 0)
        {
            bool firstDailyElement = true;
            uri.Query += "&daily=";
            foreach (var option in options.Daily)
            {
                if (firstDailyElement)
                {
                    uri.Query += option.ToString();
                    firstDailyElement = false;
                }
                else
                {
                    uri.Query += "," + option.ToString();
                }
            }
        }

        // 0.2.0 Weather models
        // cell_selection
        uri.Query += "&cell_selection=" + options.Cell_Selection;

        // new current parameter
        if (options.Current != null && options.Current.Count > 0)
        {
            bool firstCurrentElement = true;
            uri.Query += "&current=";
            foreach (var option in options.Current)
            {
                if (firstCurrentElement)
                {
                    uri.Query += option.ToString();
                    firstCurrentElement = false;
                }
                else
                {
                    uri.Query += "," + option.ToString();
                }
            }
        }

        return uri.ToString();
    }
}
