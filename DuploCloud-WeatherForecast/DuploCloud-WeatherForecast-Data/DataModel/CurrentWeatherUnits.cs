

namespace DuploCloud_WeatherForecast_Data.DataModel;

public class CurrentWeatherUnits
{
    public string? Time { get; set; }
    public string? Interval { get; set; }
    public string? Temperature_2m { get; set; }
    public string? Temperature { get { return Temperature_2m; } private set { } }
    public string? RelativeHumidity_2m { get; set; }
    public string? ApparentTemperature { get; set; }
    public string? IsDayOrNight { get; set; }
    public string? Precipitation { get; set; }
    public string? Rain { get; set; }
    public string? Showers { get; set; }
    public string? Snowfall { get; set; }
    public string? Weathercode { get; set; }
    public string? Cloudcover { get; set; }
    public string? SealevelPressure { get; set; }
    public string? SurfacePressure { get; set; }
    public string? WindSpeed_10m { get; set; }
    public string? WindDirection_10m { get; set; }
    public string? WindGusts_10m { get; set; }
}
