using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{zipCode}")]
    [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetWeather(string zipCode)
    {
        if (!_weatherService.IsValidZipCode(zipCode))
        {
            return BadRequest(new { error = "Invalid zip code format. Please provide a 5-digit US zip code." });
        }

        var weather = _weatherService.GetWeatherByZipCode(zipCode);

        if (weather is null)
        {
            return NotFound(new { error = $"Weather data not found for zip code: {zipCode}" });
        }

        return Ok(weather);
    }
}
