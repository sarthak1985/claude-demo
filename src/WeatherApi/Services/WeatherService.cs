using System.Text.RegularExpressions;
using WeatherApi.Data;
using WeatherApi.Models;

namespace WeatherApi.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherDataStore _dataStore;
    private static readonly Regex ZipCodeRegex = new(@"^\d{5}$", RegexOptions.Compiled);

    public WeatherService(IWeatherDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public bool IsValidZipCode(string zipCode)
    {
        return !string.IsNullOrWhiteSpace(zipCode) && ZipCodeRegex.IsMatch(zipCode);
    }

    public WeatherResponse? GetWeatherByZipCode(string zipCode)
    {
        var data = _dataStore.GetWeatherByZipCode(zipCode);

        if (data is null)
            return null;

        return new WeatherResponse(
            data.ZipCode,
            data.CityName,
            data.TemperatureFahrenheit,
            data.FeelsLikeFahrenheit,
            data.Condition,
            data.HumidityPercent,
            data.WindSpeedMph
        );
    }
}
