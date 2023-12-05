using DuploCloud_WeatherForecast.OptionsManagers;
using DuploCloud_WeatherForecast_Data.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DuploCloud_WeatherForecastTest;

[TestClass]
public class HourlyOptionsManagerTests
{
    [TestMethod]
    public void HourlyOptionsManager_Add_One_Parameter_Test()
    {
        var options = new HourlyOptionsManager();

        Assert.AreEqual(0, options.Parameter.Count);
        options.Add(HourlyOptionsParameter.winddirection_80m);
        Assert.AreEqual(1, options.Parameter.Count);
        Assert.IsTrue(options.Parameter.Contains(HourlyOptionsParameter.winddirection_80m));

    }

    [TestMethod]
    public void HourlyOptionsManager_Add_Existing_Parameter_Test()
    {
        var options = new HourlyOptionsManager();

        options.Add(HourlyOptionsParameter.soil_moisture_3_9cm);
        options.Add(HourlyOptionsParameter.soil_moisture_3_9cm);

        Assert.AreEqual(1, options.Count);
    }

    [TestMethod]
    public void Daily_All_Hourly_All_Test()
    {
        var options = new WeatherForecastOptionsManager(10.5f, 20f);
        options.Daily = DailyOptionsManager.All;
        options.Hourly = HourlyOptionsManager.All;

        Assert.IsTrue(options.Daily.Parameter.Count > 0);
        Assert.IsTrue(options.Hourly.Parameter.Count > 0);

        foreach (var dailyOption in (DailyOptionsParameter[])Enum.GetValues(typeof(DailyOptionsParameter)))
        {
            Assert.IsTrue(options.Daily.Contains(dailyOption));
        }

        foreach (var hourlyOption in (HourlyOptionsParameter[])Enum.GetValues(typeof(HourlyOptionsParameter)))
        {
            Assert.IsTrue(options.Hourly.Contains(hourlyOption));
        }
    }

    [TestMethod]
    public void HourlyOptionsManager_Add_Already_Added_Test()
    {
        var hourly = HourlyOptionsManager.All;
        int oldCount = hourly.Count;

        hourly.Add(HourlyOptionsParameter.cloudcover);

        Assert.AreEqual(oldCount, hourly.Count);
    }
}