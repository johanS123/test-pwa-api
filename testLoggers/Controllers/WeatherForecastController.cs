using Microsoft.AspNetCore.Mvc;
using testLoggers.Models;

namespace testLoggers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        TestLoggerCTX ctx;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TestLoggerCTX _ctx)
        {
            _logger = logger;
            ctx = _ctx;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Users> Get()
        {
            return ctx.Users.ToList();
        }
    }
}
