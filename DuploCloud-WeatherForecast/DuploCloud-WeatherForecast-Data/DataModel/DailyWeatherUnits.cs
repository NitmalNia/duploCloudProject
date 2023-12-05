

namespace DuploCloud_WeatherForecast_Data.DataModel;

public class DailyWeatherUnits
{
    public string? Time { get; set; }
    public string? Weathercode { get; set; }
    public string? MaximumTemperature_2m { get; set; }
    public string? MiniumTemperature_2m { get; set; }
    public string? MaximumApparentTemperature_2m { get; set; }
    public string? MinimumApparentTemperature_2m { get; set; }
    public string? Sunrise { get; set; }
    public string? Sunset { get; set; }
    public string? DaylightDuration { get; set; }
    public string? SunshineDuration { get; set; }
    public string? UVIndex { get; set; }
    public string? UVIndexClearSky { get; set; }
    public string? PrecipitationSum { get; set; }
    public string? RainSum { get; set; }
    public string? ShowersSum { get; set; }
    public string? SnowfallSum { get; set; }
    public string? PrecipitationHours { get; set; }
    public string? MaximumWindspeed_10m { get; set; }
    public string? MaximumWindgusts_10m { get; set; }
    public string? DominantWinddirection_10m { get; set; }
    public string? ShortwaveRadiationSum { get; set; }
    public string? ReferenceEvapotranspiration_et0 { get; set; }
}
