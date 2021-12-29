using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Services;

namespace webapp.Services
{
    public class WeatherForcastService : IWeatherForcastService
    {
        private readonly HttpClient _httpClient;
        private readonly string _WeatherFoecastScope = string.Empty;
        private readonly string _WeatherForcastBaseAddress = string.Empty;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IHttpContextAccessor _contextAccessor;


        public WeatherForcastService( IConfiguration configuration
                                        //, ITokenAcquisition tokenAcquisition 
                                        //,IHttpContextAccessor contextAccessor
            )
        {
            _httpClient = new HttpClient();
            _WeatherForcastBaseAddress = configuration["TodoList:TodoListBaseAddress"];
            //_WeatherFoecastScope = configuration["TodoList:TodoListScope"];
            //_tokenAcquisition = tokenAcquisition;
            //_contextAccessor = contextAccessor;
        }
        public async Task<List<WeatherForecast>> GetWeatherAsync()
        {
            await PrepareAuthenticatedClient();

            var response = await _httpClient.GetAsync($"{ _WeatherForcastBaseAddress}/api/WeatherForecast");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<WeatherForecast> weatherForcastlist = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);


                return weatherForcastlist;
            }

            throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
        }

        private async Task PrepareAuthenticatedClient()
        {
            //var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _WeatherFoecastScope });
            //Debug.WriteLine($"access token-{accessToken}");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
