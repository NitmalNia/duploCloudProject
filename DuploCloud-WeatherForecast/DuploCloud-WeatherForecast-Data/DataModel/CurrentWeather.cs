

namespace DuploCloud_WeatherForecast_Data.DataModel;

public class CurrentWeather
{
    public string? Time { get; set; }
    public int? Interval { get; set; }
    public float? Temperature { get { return Temperature_2m; } private set { } }
    public float? Temperature_2m { get; set; }
    public int? Weathercode { get; set; }
    public float? WindSpeed_10m { get; set; }
    public int? WindDirection_10m { get; set; }
    public float? WindGusts_10m { get; set; }
    public int? RelativeHumidity_2m { get; set; }
    public float? ApparentTemperature { get; set; }
    public int? IsDayOrNight { get; set; }
    public float? Precipitation { get; set; }
    public float? Rain { get; set; }
    public float? Showers { get; set; }
    public float? Snowfall { get; set; }
    public int? Cloudcover { get; set; }
    public float? SealevelPressure { get; set; }
    public float? SurfacePressure { get; set; }
}
