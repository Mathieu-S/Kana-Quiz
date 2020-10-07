using System.Collections.Generic;
using KanaQuiz.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KanaQuiz.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Kana> Get()
        {
            return new List<Kana>
            {
                new Kana {Id = 1, Type = KanaType.Hiragana, Romanji = "A", Value = "a"}
            };
        }
    }
}