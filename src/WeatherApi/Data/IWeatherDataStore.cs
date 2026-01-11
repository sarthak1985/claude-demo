namespace WeatherApi.Data;

public interface IWeatherDataStore
{
    CityWeatherData? GetWeatherByZipCode(string zipCode);
    bool ZipCodeExists(string zipCode);
}
