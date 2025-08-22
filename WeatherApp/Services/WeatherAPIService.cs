using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherAPIService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "0822a55a2037b69e103d661be1b0216c";

        public WeatherAPIService() { _httpClient = new HttpClient(); }

        public async Task<WeatherInformation> GetWeatherByCityAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={ApiKey}&units=metric";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<WeatherInformation>(json);
        }

        public async Task<WeatherInformation> GetWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={ApiKey}&units=metric";
            var json = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<WeatherInformation>(json);
        }
    }
}
