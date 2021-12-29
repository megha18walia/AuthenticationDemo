using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using webapi;

namespace ConsoleApp1
{
    public class FetchData
    {
        private static readonly string TodoListBaseAddress = "https://localhost:44324/";

        private static readonly string AadInstance = "https://login.microsoftonline.com/{0}/v2.0";
        private static readonly string Tenant = "3d73e268-e9b4-4525-96dc-6739e4b29a69";
        private static readonly string ClientId = "b234db4b-61b5-40f2-845b-870ffd8a561e";

        private static readonly string Authority = string.Format(CultureInfo.InvariantCulture, AadInstance, Tenant);

        // To authenticate to the To Do list service, the client needs to know the service's App ID URI and URL

        private static readonly string TodoListScope = "api://2e42e1ee-41d3-4312-836d-974d47285068/taskread";
        private static readonly string[] Scopes = { TodoListScope };
        private readonly IPublicClientApplication _app;


        private readonly HttpClient _httpClient = new HttpClient();

        private static string TodoListApiAddress
        {
            get
            {
                string baseAddress = TodoListBaseAddress;
                return baseAddress.EndsWith("/") ? TodoListBaseAddress + "api/Weatherforecast"
                                                 : TodoListBaseAddress + "/api/Weatherforecast";
            }
        }

        public FetchData()
        {
            _app = PublicClientApplicationBuilder.Create(ClientId)
               .WithAuthority(Authority)
               .WithRedirectUri("http://localhost") // needed only for the system browser
               .Build();
        }
        public async Task PrintWeatherForecast()
        {

            try {
                AuthenticationResult result = null;
                try
                {
                    var accounts = (await _app.GetAccountsAsync()).ToList();
                    var builder = _app.AcquireTokenInteractive(Scopes)
                        .WithAccount(accounts.FirstOrDefault())
                        .WithPrompt(Prompt.SelectAccount);

                    if (!_app.IsEmbeddedWebViewAvailable())
                    {
                        // You app should install the embedded browser WebView2 https://aka.ms/msal-net-webview2
                        // but if for some reason this is not possible, you can fall back to the system browser 
                        // in this case, the redirect uri needs to be set to "http://localhost"
                        builder = builder.WithUseEmbeddedWebView(false);
                    }

                     result = await builder.ExecuteAsync().ConfigureAwait(false);


                }
                // There is no access token in the cache, so prompt the user to sign-in.
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                 _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

                // Call the To Do list service.
                HttpResponseMessage response = await _httpClient.GetAsync(TodoListApiAddress);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response and data-bind to the GridView to display To Do items.
                    string s = await response.Content.ReadAsStringAsync();
                    List<WeatherForecast> toDoArray = JsonConvert.DeserializeObject<List<WeatherForecast>>(s);
                    foreach (var w in toDoArray)
                    {
                        Console.WriteLine(w.TemperatureF + " " + w.Summary + " " + w.Date.ToString() + " " + w.TemperatureC);
                    }

                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
