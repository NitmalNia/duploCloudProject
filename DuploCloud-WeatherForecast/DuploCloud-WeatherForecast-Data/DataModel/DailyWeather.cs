

namespace DuploCloud_WeatherForecast_Data.DataModel;

public class DailyWeather
{
    public string[]? Time { get; set; }
    public float[]? Weathercode { get; set; }
    public float[]? MaximumTemperature_2m { get; set; }
    public float[]? MiniumTemperature_2m { get; set; }
    public float[]? MaximumApparentTemperature_2m { get; set; }
    public float[]? MinimumApparentTemperature_2m { get; set; }
    public string[]? Sunrise { get; set; }
    public string[]? Sunset { get; set; }
    public float[]? DaylightDuration { get; set; }
    public float[]? SunshineDuration { get; set; }
    public float[]? UVIndex { get; set; }
    public float[]? UVIndexClearSky { get; set; }
    public float[]? PrecipitationSum { get; set; }
    public float[]? RainSum { get; set; }
    public float[]? ShowersSum { get; set; }
    public float[]? SnowfallSum { get; set; }
    public float[]? PrecipitationHours { get; set; }
    public float[]? MaximumWindspeed_10m { get; set; }
    public float[]? MaximumWindgusts_10m { get; set; }
    public float[]? DominantWinddirection_10m { get; set; }
    public float[]? ShortwaveRadiationSum { get; set; }
    public float[]? ReferenceEvapotranspiration_et0 { get; set; }
}
