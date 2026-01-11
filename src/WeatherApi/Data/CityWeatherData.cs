using WeatherApi.Models;

namespace WeatherApi.Data;

public record CityWeatherData(
    string ZipCode,
    string CityName,
    string State,
    double TemperatureFahrenheit,
    double FeelsLikeFahrenheit,
    WeatherCondition Condition,
    int HumidityPercent,
    double WindSpeedMph
);
