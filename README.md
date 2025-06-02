# ApiAggregatorService

ApiAggregatorService is a .NET 9 Web API designed to aggregate and orchestrate data from multiple external APIs into a unified interface. It provides a single endpoint for clients to retrieve, combine, and process data from various sources, simplifying integration and reducing client-side complexity.

## Features

- Aggregates responses from multiple APIs
- Centralized error handling and logging
- Extensible client and service architecture
- Built-in support for caching and resiliency (using Polly)
- Interactive API documentation with Swagger (OpenAPI)

## Getting Started

1. **Clone the repository**
2. **Configure API endpoints and credentials in `appsettings.json`**
3. **Restore the project:**
   ```sh
   dotnet restore
   ```
3. **Run the project:**
   ```sh
   dotnet run --project ApiAggregator.csproj
   ```
4. **Access the Swagger UI:**  
   Visit [https://localhost:7249/swagger/index.html](https://localhost:7249/swagger/index.html) in your browser.

## Running with Docker

1. **Change to root Directory (where docker-compose.yml is located)**
    ```sh
   cd ..
   ```
2. **Build the Docker image:**
   ```sh
   docker compose build
   ```
3. **Run the container:**
   ```sh
   docker compose up -d
   ```
4. **Access the API:**  
   Visit [http://localhost:7249/swagger/index.html](http://localhost:7249/swagger/index.html) in your browser.

## Project Structure

- `source_code/ApiAggregator.Api/Clients/` – API client implementations
- `source_code/ApiAggregator.Api/Controllers/` – API controllers
- `source_code/ApiAggregator.Api/Middleware/` – Custom middleware
- `source_code/ApiAggregator.Api/Services/` – Business logic and aggregation services

## Dependencies

- ASP.NET Core 9
- Swashbuckle.AspNetCore (Swagger/OpenAPI)
- Polly (resiliency and transient-fault-handling)
- Microsoft.Extensions.* packages

## License

MIT License