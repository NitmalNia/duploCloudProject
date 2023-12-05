using DuploCloud_WeatherForecast.OptionsManagers;
using DuploCloud_WeatherForecast_Data.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace DuploCloud_WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<WeatherForecast?> Get(float latitude, float longitude)
        {
            WeatherForecast weatherForecast = new WeatherForecast();
            OpenMeteoClient meteoClient = new OpenMeteoClient();
            if(latitude > 0 && longitude > 0)
            {
                weatherForecast = await meteoClient.QueryAsync(latitude, longitude);
            }
            return weatherForecast;
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public async Task<WeatherForecast?> Post(WeatherForecastOptionsManager options)
        {
            WeatherForecast weatherForecast = new WeatherForecast();
            OpenMeteoClient meteoClient = new OpenMeteoClient();
            if (options.Latitude > 0 && options.Latitude > 0)
            {
                weatherForecast = await meteoClient.QueryAsync(options);
            }
            return weatherForecast;
        }
    }
}