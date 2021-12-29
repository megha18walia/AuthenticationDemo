using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            FetchData f = new FetchData();
            await f.PrintWeatherForecast();
        }
    }
}
