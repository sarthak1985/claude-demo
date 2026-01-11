using WeatherApi.Models;

namespace WeatherApi.Data;

public class MockWeatherDataStore : IWeatherDataStore
{
    private static readonly Dictionary<string, CityWeatherData> _weatherData = new()
    {
        ["10001"] = new CityWeatherData("10001", "New York", "NY", 72.5, 75.0, WeatherCondition.Sunny, 45, 8.5),
        ["90210"] = new CityWeatherData("90210", "Beverly Hills", "CA", 82.0, 80.0, WeatherCondition.Sunny, 30, 5.0),
        ["60601"] = new CityWeatherData("60601", "Chicago", "IL", 65.0, 62.0, WeatherCondition.Cloudy, 60, 15.0),
        ["98101"] = new CityWeatherData("98101", "Seattle", "WA", 55.0, 52.0, WeatherCondition.Rainy, 85, 10.0),
        ["33101"] = new CityWeatherData("33101", "Miami", "FL", 88.0, 95.0, WeatherCondition.PartlyCloudy, 75, 12.0),
        ["80202"] = new CityWeatherData("80202", "Denver", "CO", 58.0, 55.0, WeatherCondition.Sunny, 25, 7.0),
        ["02101"] = new CityWeatherData("02101", "Boston", "MA", 62.0, 60.0, WeatherCondition.Cloudy, 55, 18.0),
        ["85001"] = new CityWeatherData("85001", "Phoenix", "AZ", 105.0, 102.0, WeatherCondition.Sunny, 15, 4.0),
        ["30301"] = new CityWeatherData("30301", "Atlanta", "GA", 78.0, 82.0, WeatherCondition.PartlyCloudy, 65, 6.0),
        ["75201"] = new CityWeatherData("75201", "Dallas", "TX", 92.0, 98.0, WeatherCondition.Sunny, 40, 9.0)
    };

    public CityWeatherData? GetWeatherByZipCode(string zipCode)
    {
        _weatherData.TryGetValue(zipCode, out var data);
        return data;
    }

    public bool ZipCodeExists(string zipCode)
    {
        return _weatherData.ContainsKey(zipCode);
    }
}
