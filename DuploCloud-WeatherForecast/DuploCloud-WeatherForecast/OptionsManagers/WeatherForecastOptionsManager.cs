using DuploCloud_WeatherForecast_Common.Enums;

namespace DuploCloud_WeatherForecast.OptionsManagers;

public class WeatherForecastOptionsManager
{
    public float Latitude { get; set; }

    public float Longitude { get; set; }
    public TemperatureUnitType? Temperature_Unit { get; set; }

    public WindspeedUnitType? Windspeed_Unit { get; set; }
    public PrecipitationUnitType? Precipitation_Unit { get; set; }
    public CellSelectionType? Cell_Selection { get; set; }

    public string? Timezone { get; set; }

    public HourlyOptionsManager? Hourly { get { return _hourly; } set { if (value != null) _hourly = value; } }
    public DailyOptionsManager? Daily { get { return _daily; } set { if (value != null) _daily = value; } }
    public CurrentOptionsManager? Current { get { return _current; } set { if (value != null) _current = value; } }
    public TimeformatType? Timeformat { get; set; }
    public int? Past_Days { get; set; }
    public string? Start_date { get; set; }
    public string? End_date { get; set; }

    private HourlyOptionsManager _hourly = new HourlyOptionsManager();
    private DailyOptionsManager _daily = new DailyOptionsManager();
    private CurrentOptionsManager _current = new CurrentOptionsManager();

    public WeatherForecastOptionsManager(float latitude, float longitude, TemperatureUnitType temperature_Unit, WindspeedUnitType windspeed_Unit, PrecipitationUnitType precipitation_Unit, string timezone, HourlyOptionsManager hourly, DailyOptionsManager daily, CurrentOptionsManager current, TimeformatType timeformat, int past_Days, string start_date, string end_date, CellSelectionType cell_selection)
    {
        Latitude = latitude;
        Longitude = longitude;
        Temperature_Unit = temperature_Unit;
        Windspeed_Unit = windspeed_Unit;
        Precipitation_Unit = precipitation_Unit;
        Timezone = timezone;

        if (hourly != null)
            Hourly = hourly;
        if (daily != null)
            Daily = daily;
        if (current != null)
            Current = current;

        Timeformat = timeformat;
        Past_Days = past_Days;
        Start_date = start_date;
        End_date = end_date;
        Cell_Selection = cell_selection;
    }
    public WeatherForecastOptionsManager(float latitude, float longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        Temperature_Unit = TemperatureUnitType.celsius;
        Windspeed_Unit = WindspeedUnitType.kmh;
        Precipitation_Unit = PrecipitationUnitType.mm;
        Timeformat = TimeformatType.iso8601;
        Cell_Selection = CellSelectionType.land;
        Timezone = "GMT";

        Start_date = string.Empty;
        End_date = string.Empty;
    }
    public WeatherForecastOptionsManager()
    {
        Latitude = 0f;
        Longitude = 0f;
        Temperature_Unit = TemperatureUnitType.celsius;
        Windspeed_Unit = WindspeedUnitType.kmh;
        Precipitation_Unit = PrecipitationUnitType.mm;
        Timeformat = TimeformatType.iso8601;
        Cell_Selection = CellSelectionType.land;
        Timezone = "GMT";

        Start_date = string.Empty;
        End_date = string.Empty;
    }
}
