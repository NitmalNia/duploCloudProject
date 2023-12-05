
namespace DuploCloud_WeatherForecast_Data.DataModel;

public class HourlyWeather
{
    public string[]? Time { get; set; }
    public float[]? Temperature_2m { get; set; }
    public int[]? RelativeHumidity_2m { get; set; }
    public float[]? Dewpoint_2m { get; set; }
    public float[]? ApparentTemperature { get; set; }
    public float[]? PrecipitationProbability { get; set; }
    public float[]? Rain { get; set; }
    public float[]? Showers { get; set; }
    public float[]? Snowfall { get; set; }
    public float[]? SnowDepth { get; set; }
    public int[]? Weathercode { get; set; }
    public float[]? SealevelPressure { get; set; }
    public float[]? SurfacePressure { get; set; }
    public int[]? Cloudcover { get; set; }
    public int[]? CloudcoverLow { get; set; }
    public int[]? CloudcoverMid { get; set; }
    public int[]? CloudcoverHigh { get; set; }
    public float[]? Visibility { get; set; }
    public float[]? Evapotranspiration { get; set; }
    public float[]? WindSpeed_10m { get; set; }
    public float[]? WindSpeed_80m { get; set; }
    public float[]? WindSpeed_120m { get; set; }
    public float[]? WindSpeed_180m { get; set; }
    public int[]? WindDirection_10m { get; set; }
    public int[]? WindDirection_80m { get; set; }
    public int[]? WindDirection_120m { get; set; }
    public int[]? WindDirection_180m { get; set; }
    public float[]? WindGusts_10m { get; set; }
    public float[]? Temperature_80m { get; set; }
    public float[]? Temperature_120m { get; set; }
    public float[]? Temperature_180m { get; set; }
    public float[]? SoilTemperature_0cm { get; set; }
    public float[]? SoilTemperature_6cm { get; set; }
    public float[]? SoilTemperature_18cm { get; set; }
    public float[]? SoilTemperature_54cm { get; set; }
    public float[]? SoilMoisture_0_1cm { get; set; }
    public float[]? SoilMoisture_1_3cm { get; set; }
    public float[]? SoilMoisture_3_9cm { get; set; }
    public float[]? SoilMoisture_9_27cm { get; set; }
    public float[]? SoilMoisture_27_81cm { get; set; }
}
