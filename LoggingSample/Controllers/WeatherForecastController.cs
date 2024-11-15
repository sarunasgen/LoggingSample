using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections;

namespace LoggingSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController()
        {
            
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Log.Information("GetWeatherForecast request recieved");
            IEnumerable<WeatherForecast> result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            foreach(var e in result)
            {
                Log.Debug($"GetWeatherForecast returned item: {e.Date} {e.TemperatureC} {e.Summary}");
            }
            Log.Information("GetWeatherForecast Request completed");
            try
            {
                WeatherForecast x = result.ElementAt(7);
            }
            catch(Exception e)
            {
                Log.Error($"Could not find element at index 7. Exception thrown {e.Message}");
            }
            return result;
        }
    }
}
