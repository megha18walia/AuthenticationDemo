using System.Collections.Generic;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.Services
{
    public interface IWeatherForcastService
    {
        Task<List<WeatherForecast>> GetWeatherAsync();

    }
}
