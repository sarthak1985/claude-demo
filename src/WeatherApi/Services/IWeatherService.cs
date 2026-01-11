using WeatherApi.Models;

namespace WeatherApi.Services;

public interface IWeatherService
{
    WeatherResponse? GetWeatherByZipCode(string zipCode);
    bool IsValidZipCode(string zipCode);
}
