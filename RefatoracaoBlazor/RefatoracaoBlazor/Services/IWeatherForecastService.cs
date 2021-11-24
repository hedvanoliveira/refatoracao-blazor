using RefatoracaoBlazor.Models;

namespace RefatoracaoBlazor.Services
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>?> GetAsync();
    }
}