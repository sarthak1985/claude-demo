using WeatherApi.Data;
using Xunit;

namespace WeatherApi.Tests.Data;

public class MockWeatherDataStoreTests
{
    private readonly MockWeatherDataStore _sut;

    public MockWeatherDataStoreTests()
    {
        _sut = new MockWeatherDataStore();
    }

    [Fact]
    public void GetWeatherByZipCode_WithExistingZip_ReturnsData()
    {
        // Act
        var result = _sut.GetWeatherByZipCode("10001");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New York", result.CityName);
        Assert.Equal("NY", result.State);
    }

    [Fact]
    public void GetWeatherByZipCode_WithNonExistingZip_ReturnsNull()
    {
        // Act
        var result = _sut.GetWeatherByZipCode("00000");

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData("10001", true)]
    [InlineData("90210", true)]
    [InlineData("60601", true)]
    [InlineData("00000", false)]
    [InlineData("99999", false)]
    public void ZipCodeExists_ReturnsExpectedResult(string zipCode, bool expected)
    {
        // Act
        var result = _sut.ZipCodeExists(zipCode);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetWeatherByZipCode_ReturnsAllExpectedFields()
    {
        // Act
        var result = _sut.GetWeatherByZipCode("10001");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("10001", result.ZipCode);
        Assert.Equal("New York", result.CityName);
        Assert.Equal("NY", result.State);
        Assert.Equal(72.5, result.TemperatureFahrenheit);
        Assert.Equal(75.0, result.FeelsLikeFahrenheit);
        Assert.Equal(45, result.HumidityPercent);
        Assert.Equal(8.5, result.WindSpeedMph);
    }
}
