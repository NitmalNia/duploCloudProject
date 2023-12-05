
using System.Text.Json.Serialization;

namespace DuploCloud_WeatherForecast_Data.DataModel;

public class WeatherForecast
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public float Elevation { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public float GenerationTime { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffset { get; set; }
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    [JsonPropertyName("current")]
    public CurrentWeather? Current { get; set; }

    [JsonPropertyName("current_units")]
    public CurrentWeatherUnits? CurrentUnits { get; set; }

    [JsonPropertyName("hourly_units")]
    public HourlyWeatherUnits? HourlyUnits { get; set; }

    [JsonPropertyName("hourly")]
    public HourlyWeather? Hourly { get; set; }

    [JsonPropertyName("daily_units")]
    public DailyWeatherUnits? DailyUnits { get; set; }

    [JsonPropertyName("daily")]
    public DailyWeather? Daily { get; set; }
}
