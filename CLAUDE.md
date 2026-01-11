# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test Commands

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run a single test by name
dotnet test --filter "FullyQualifiedName~WeatherServiceTests.IsValidZipCode_ReturnsExpectedResult"

# Run the API (available at http://localhost:5229)
dotnet run --project src/WeatherApi

# Swagger UI available in development mode at /swagger
```

## Architecture

This is a .NET 6 Web API for weather data lookup by US zip code.

**Layered structure:**
- **Controllers** (`WeatherController`) - HTTP endpoints, input validation, response formatting
- **Services** (`IWeatherService`/`WeatherService`) - Business logic, zip code validation
- **Data** (`IWeatherDataStore`/`MockWeatherDataStore`) - Data access abstraction with in-memory mock implementation

**Key patterns:**
- Constructor injection for all dependencies
- Interface-based abstractions for testability (Moq used in tests)
- Record types for immutable DTOs (`WeatherResponse`, `CityWeatherData`)
- xUnit for testing with Theory/InlineData for parameterized tests
