using RefatoracaoBlazor.Models;
using System.Net.Http.Json;

namespace RefatoracaoBlazor.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private HttpClient _httpClient { get; }
        private readonly IConfiguration _config;

        public WeatherForecastService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<WeatherForecast>?> GetAsync()
        {
            string url = _config.GetValue<string>("ApiWeatherForecast");

            var forecasts = await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(url);

            return forecasts;
        }
    }
}
