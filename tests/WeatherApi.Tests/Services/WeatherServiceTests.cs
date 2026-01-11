using Moq;
using WeatherApi.Data;
using WeatherApi.Models;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests.Services;

public class WeatherServiceTests
{
    private readonly Mock<IWeatherDataStore> _mockDataStore;
    private readonly WeatherService _sut;

    public WeatherServiceTests()
    {
        _mockDataStore = new Mock<IWeatherDataStore>();
        _sut = new WeatherService(_mockDataStore.Object);
    }

    [Theory]
    [InlineData("10001", true)]
    [InlineData("90210", true)]
    [InlineData("00000", true)]
    [InlineData("12345", true)]
    [InlineData("1234", false)]      // Too short
    [InlineData("123456", false)]    // Too long
    [InlineData("1234a", false)]     // Contains letter
    [InlineData("", false)]          // Empty
    [InlineData("  ", false)]        // Whitespace
    [InlineData("abcde", false)]     // All letters
    public void IsValidZipCode_ReturnsExpectedResult(string zipCode, bool expected)
    {
        // Act
        var result = _sut.IsValidZipCode(zipCode);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsValidZipCode_WithNull_ReturnsFalse()
    {
        // Act
        var result = _sut.IsValidZipCode(null!);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetWeatherByZipCode_WithValidZip_ReturnsWeatherResponse()
    {
        // Arrange
        var testData = new CityWeatherData("10001", "New York", "NY", 72.5, 75.0,
            WeatherCondition.Sunny, 45, 8.5);
        _mockDataStore.Setup(x => x.GetWeatherByZipCode("10001")).Returns(testData);

        // Act
        var result = _sut.GetWeatherByZipCode("10001");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("10001", result.ZipCode);
        Assert.Equal("New York", result.CityName);
        Assert.Equal(72.5, result.TemperatureFahrenheit);
        Assert.Equal(75.0, result.FeelsLikeFahrenheit);
        Assert.Equal(WeatherCondition.Sunny, result.Condition);
        Assert.Equal(45, result.HumidityPercent);
        Assert.Equal(8.5, result.WindSpeedMph);
    }

    [Fact]
    public void GetWeatherByZipCode_WithUnknownZip_ReturnsNull()
    {
        // Arrange
        _mockDataStore.Setup(x => x.GetWeatherByZipCode("99999")).Returns((CityWeatherData?)null);

        // Act
        var result = _sut.GetWeatherByZipCode("99999");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetWeatherByZipCode_CallsDataStore()
    {
        // Arrange
        _mockDataStore.Setup(x => x.GetWeatherByZipCode("10001")).Returns((CityWeatherData?)null);

        // Act
        _sut.GetWeatherByZipCode("10001");

        // Assert
        _mockDataStore.Verify(x => x.GetWeatherByZipCode("10001"), Times.Once);
    }
}
