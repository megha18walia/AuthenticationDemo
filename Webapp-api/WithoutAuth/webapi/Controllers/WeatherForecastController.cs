using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Controllers
{
  //  [Authorize]  
    [ApiController]
    [Route("api/[controller]")]
  //  [RequiredScope(scopeRequiredByAPI)]
    public class WeatherForecastController : ControllerBase
    {
        const string scopeRequiredByAPI = "readdata";
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

    //    private readonly IHttpContextAccessor _contextAccessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            //, IHttpContextAccessor contextAccessor
            )
        {
   //         this._contextAccessor = contextAccessor;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
              //  Summary = string.Concat(Summaries[rng.Next(Summaries.Length)]," UserName : ", (new List<System.Security.Claims.Claim>(_contextAccessor.HttpContext.User.Claims)[5]).Value)
            })
            .ToArray();
        }
    }
}
