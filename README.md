# Weather API

A .NET 6 Web API that provides weather data by US zip code.

## Features

- Get weather information by 5-digit US zip code
- Returns temperature, feels-like temperature, humidity, wind speed, and weather condition
- Swagger UI for API exploration
- Comprehensive unit test coverage

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Running the API

```bash
# Clone the repository
git clone https://github.com/sarthak1985/claude-demo.git
cd claude-demo

# Build the solution
dotnet build

# Run the API
dotnet run --project src/WeatherApi
```

The API will be available at `http://localhost:5229`

Swagger UI: `http://localhost:5229/swagger`

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with verbose output
dotnet test --verbosity normal
```

## API Endpoints

### Get Weather by Zip Code

```
GET /api/weather/{zipCode}
```

**Parameters:**
- `zipCode` (path) - 5-digit US zip code

**Response (200 OK):**
```json
{
  "zipCode": "10001",
  "cityName": "New York",
  "temperatureFahrenheit": 72.5,
  "feelsLikeFahrenheit": 75.0,
  "condition": "Sunny",
  "humidityPercent": 45,
  "windSpeedMph": 8.5
}
```

**Error Responses:**
- `400 Bad Request` - Invalid zip code format
- `404 Not Found` - Weather data not available for the zip code

### Supported Zip Codes

The mock data store includes weather for these cities:

| Zip Code | City |
|----------|------|
| 10001 | New York, NY |
| 90210 | Beverly Hills, CA |
| 60601 | Chicago, IL |
| 98101 | Seattle, WA |
| 33101 | Miami, FL |
| 80202 | Denver, CO |
| 02101 | Boston, MA |
| 85001 | Phoenix, AZ |
| 30301 | Atlanta, GA |
| 75201 | Dallas, TX |

## Project Structure

```
├── src/
│   └── WeatherApi/
│       ├── Controllers/     # API endpoints
│       ├── Services/        # Business logic
│       ├── Data/            # Data access layer
│       ├── Models/          # DTOs and enums
│       └── Program.cs       # Application entry point
└── tests/
    └── WeatherApi.Tests/    # Unit tests
```

## License

This project is for demonstration purposes.
