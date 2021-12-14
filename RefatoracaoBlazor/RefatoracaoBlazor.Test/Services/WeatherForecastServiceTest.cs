using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using RefatoracaoBlazor.Models;
using RefatoracaoBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RefatoracaoBlazor.Test.Services
{
    public class WeatherForecastServiceTest
    {
        
        public WeatherForecastServiceTest()
        {

        }

        [Fact]
        public async Task WeatherForecastServiceGetAsync()
        {
            //Arrange
            //
            List<WeatherForecast> weatherForecastList = new();
            weatherForecastList.Add(new WeatherForecast() { Date = DateTime.Parse("2018-05-06"), Summary = "Freezing", TemperatureC = 1 });
            weatherForecastList.Add(new WeatherForecast() { Date = DateTime.Parse("2018-05-07"), Summary = "Bracing", TemperatureC = 14 });
            string jsonString = JsonSerializer.Serialize(weatherForecastList);
            //

            //
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonString)
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            //

            //
            var configurationSectionMock = new Mock<IConfigurationSection>();
            var configurationMock = new Mock<IConfiguration>();

            configurationSectionMock
               .Setup(x => x.Value)
               .Returns("http://localhost");

            configurationMock
               .Setup(x => x.GetSection("ApiWeatherForecast"))
               .Returns(configurationSectionMock.Object);
            //

            //Act
            var weatherForecastService = new WeatherForecastService(httpClient, configurationMock.Object);

            var result = await weatherForecastService.GetAsync();

            //Assert
            Assert.Equal(weatherForecastList.Count, result.Count);

        }


    }
}
