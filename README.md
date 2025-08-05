# Transpo Management API

## Overview
A .NET 8 Web API for managing transportation fares, vehicles, and trips. Built with layered architecture, Entity Framework Core, AutoMapper, FluentValidation, and SQLite.

## Features
- CRUD operations for Vehicles, Trips, and FareBands
- Fare calculation logic with banded rates
- DTOs and AutoMapper for clean mapping
- Repository and Service patterns
- Global exception handling and logging
- Request validation with Data Annotations and FluentValidation
- In-memory caching for static data
- Swagger/OpenAPI documentation

## Architecture
```
           +-------------------+
           |   Controllers     |
           +-------------------+
                    |
                    v
           +-------------------+
           |       DTOs        |
           +-------------------+
                    |
                    v
           +-------------------+
           |     Services      |
           | (Interfaces & Impl)|
           +-------------------+
                    |
                    v
           +-------------------+
           |   Repositories    |
           | (Interfaces & Impl)|
           +-------------------+
                    |
                    v
           +-------------------+
           |      Models       |
           +-------------------+
                    |
                    v
           +-------------------+
           |     Mapping       |
           +-------------------+
                    |
                    v
           +-------------------+
           |    Middleware     |
           +-------------------+
                    |
                    v
           +-------------------+
           |    Validators     |
           +-------------------+
```

## Setup
1. Clone the repo
2. Run `dotnet restore`
3. Run `dotnet build`
4. Run `dotnet run` from the `TranspoManagementAPI` folder
5. Access Swagger UI at `https://localhost:5001/swagger`

## API Endpoints
- `GET /api/vehicles` — List vehicles
- `POST /api/vehicles` — Create vehicle
- `GET /api/trips` — List trips
- `POST /api/trips` — Create trip
- `GET /api/farebands` — List fare bands
- `POST /api/farebands` — Create fare band
- `POST /api/fare/calculate` — Calculate fare

## Validation
- All request DTOs use Data Annotations and FluentValidation for input validation
- Invalid requests return `400 Bad Request` with error details

## Cross-Cutting Concerns
- **Error Handling:** Global middleware catches and logs exceptions
- **Logging:** All unhandled exceptions are logged
- **Validation:** Data Annotations and FluentValidation
- **Caching:** In-memory cache for fare bands
- **Swagger:** API documentation with XML comments

## Authors
- Neven Hossam

---
For more details, see Swagger UI and XML comments in the codebase.
