using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApi.Controllers;
using WeatherApi.Models;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests.Controllers;

public class WeatherControllerTests
{
    private readonly Mock<IWeatherService> _mockService;
    private readonly WeatherController _sut;

    public WeatherControllerTests()
    {
        _mockService = new Mock<IWeatherService>();
        _sut = new WeatherController(_mockService.Object);
    }

    [Fact]
    public void GetWeather_WithValidZipAndData_ReturnsOkWithWeather()
    {
        // Arrange
        var expectedResponse = new WeatherResponse("10001", "New York", 72.5, 75.0,
            WeatherCondition.Sunny, 45, 8.5);
        _mockService.Setup(x => x.IsValidZipCode("10001")).Returns(true);
        _mockService.Setup(x => x.GetWeatherByZipCode("10001")).Returns(expectedResponse);

        // Act
        var result = _sut.GetWeather("10001") as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedResponse, result.Value);
    }

    [Fact]
    public void GetWeather_WithInvalidZipFormat_ReturnsBadRequest()
    {
        // Arrange
        _mockService.Setup(x => x.IsValidZipCode("invalid")).Returns(false);

        // Act
        var result = _sut.GetWeather("invalid") as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void GetWeather_WithValidZipButNoData_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(x => x.IsValidZipCode("99999")).Returns(true);
        _mockService.Setup(x => x.GetWeatherByZipCode("99999")).Returns((WeatherResponse?)null);

        // Act
        var result = _sut.GetWeather("99999") as NotFoundObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public void GetWeather_WithInvalidZip_DoesNotCallGetWeatherByZipCode()
    {
        // Arrange
        _mockService.Setup(x => x.IsValidZipCode("abc")).Returns(false);

        // Act
        _sut.GetWeather("abc");

        // Assert
        _mockService.Verify(x => x.GetWeatherByZipCode(It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData("10001")]
    [InlineData("90210")]
    [InlineData("60601")]
    public void GetWeather_WithDifferentValidZips_ReturnsOk(string zipCode)
    {
        // Arrange
        var expectedResponse = new WeatherResponse(zipCode, "Test City", 70.0, 70.0,
            WeatherCondition.Sunny, 50, 10.0);
        _mockService.Setup(x => x.IsValidZipCode(zipCode)).Returns(true);
        _mockService.Setup(x => x.GetWeatherByZipCode(zipCode)).Returns(expectedResponse);

        // Act
        var result = _sut.GetWeather(zipCode) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}
