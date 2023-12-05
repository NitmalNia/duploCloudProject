using DuploCloud_WeatherForecast;
using DuploCloud_WeatherForecast.OptionsManagers;
using DuploCloud_WeatherForecast_Data.DataModel;
using System.Globalization;

namespace DuploCloud_WeatherForecastTest;

[TestClass]
public class WeatherForecastTests
{

    [TestMethod]
    public async Task Latitude_Longitude_Test()
    {
        OpenMeteoClient client = new OpenMeteoClient();

        WeatherForecast weatherData = await client.QueryAsync(1.125f, 2.25f);

        Assert.IsNotNull(weatherData);
        Assert.IsNotNull(weatherData.Longitude);
        Assert.IsNotNull(weatherData.Latitude);

        Assert.AreEqual(1.125f, weatherData.Latitude);
        Assert.AreEqual(2.25f, weatherData.Longitude);
    }

    [TestMethod]
    public async Task Latitude_Longitude_Test_With_French_Culture()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

        OpenMeteoClient client = new OpenMeteoClient();

        WeatherForecast weatherData = await client.QueryAsync(1.125f, 2.25f);

        Assert.IsNotNull(weatherData);
        Assert.IsNotNull(weatherData.Longitude);
        Assert.IsNotNull(weatherData.Latitude);

        Assert.AreEqual(1.125f, weatherData.Latitude);
        Assert.AreEqual(2.25f, weatherData.Longitude);
    }

    [TestMethod]
    public async Task WeatherForecast_With_WeatherForecastOptions_Test()
    {
        OpenMeteoClient client = new();
        WeatherForecastOptionsManager weatherForecast = new();

        var res = await client.QueryAsync(weatherForecast);

        Assert.IsNotNull(res);
        Assert.AreEqual(0f, res.Latitude);
        Assert.AreEqual(0f, res.Longitude);
    }

    [TestMethod]
    public async Task WeatherForecast_With_All_Options_Test()
    {
        OpenMeteoClient client = new();
        WeatherForecastOptionsManager options = new()
        {
            Hourly = HourlyOptionsManager.All,
            Daily = DailyOptionsManager.All,
            Current = CurrentOptionsManager.All,
        };

        var res = await client.QueryAsync(options);

        Assert.IsNotNull(res);
        Assert.IsNotNull(res.Hourly);
        Assert.IsNotNull(res.HourlyUnits);
        Assert.IsNotNull(res.Daily);
        Assert.IsNotNull(res.DailyUnits);
        Assert.IsNotNull(res.Current);
    }
}
