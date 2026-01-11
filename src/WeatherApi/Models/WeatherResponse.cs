namespace WeatherApi.Models;

public record WeatherResponse(
    string ZipCode,
    string CityName,
    double TemperatureFahrenheit,
    double FeelsLikeFahrenheit,
    WeatherCondition Condition,
    int HumidityPercent,
    double WindSpeedMph
);
