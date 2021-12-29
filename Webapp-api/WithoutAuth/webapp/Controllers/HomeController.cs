using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Services;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWeatherForcastService _service;

        public HomeController(ILogger<HomeController> logger, IWeatherForcastService service)
        {
            _logger = logger;
            _service = service;
        }

    //    [AuthorizeForScopes(ScopeKeySection = "TodoList:TodoListScope")]
        public async Task<IActionResult> Index()
        {
            List<WeatherForecast> lstObj = await _service.GetWeatherAsync();
            return View(lstObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
