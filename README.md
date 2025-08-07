# Transportation Management API

## Overview
A .NET 8 Web API for managing transportation fares, vehicles, and trips. Built with layered architecture, Entity Framework Core, AutoMapper, FluentValidation, Docker, CI/CD pipeline and SQLite.

## Core Functionality 
- Transportation Fare Management: Comprehensive fare band system with configurable rate-per-mile pricing
- Vehicle Management: Full CRUD operations for managing vehicle fleet
- Trip Management: Complete trip tracking with fare calculation based on distance and fare bands
- Fare Calculation Service: Dynamic fare calculation based on distance traveled and applicable fare bands

## Performance & Security
- Rate Limiting: over than 10 requests in 60 seconds
- In-Memory Caching: Implemented for frequently accessed data (e.g., fare bands)
- Exception Handling: Global middleware for consistent error responses
- Input Validation: with Data Annotations and FluentValidation to ensure data integrity
  
## Technical Features
- Clean Architecture: Well-organized repository pattern with separation of concerns (Controllers, Services, Repositories)
- Entity Framework Core: SQLite database integration with EF Core ORM
- AutoMapper Integration: Efficient object mapping between DTOs and domain models
- Dependency Injection: Fully leveraged .NET Core DI container for loosely coupled services
- RESTful API Design: Modern API with proper HTTP verb usage and status codes
- API Documentation: Swagger/OpenAPI integration for interactive API documentation and testing
- Validation: Request validation using FluentValidation library
- DTO Pattern: Consistent use of Data Transfer Objects for API requests and responses
- Unit Testing: xUnit tests with Moq for ensuring reliability through automated regression testing

## DevOps & Deployment
- Docker Support: Complete containerization with Docker
- CI/CD Pipeline: GitHub Actions workflow for continuous integration and delivery
- Docker Hub Integration: Automated container image publishing
- Cloud Deployment Ready: Prepared to cloud platforms (already deployed on render)

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
           |(Interfaces & Impl)|
           +-------------------+
                    |
                    v
           +-------------------+
           |   Repositories    |
           |(Interfaces & Impl)|
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
5. Access Swagger UI at `https://localhost:5099/swagger`

## API Endpoints
- `GET /api/Vehicle` — List all vehicles
- `GET /api/Vehicle/{id}` — Get vehicle by ID
- `POST /api/Vehicle` — Create new vehicle
- `PUT /api/Vehicle/{id}` — Update existing vehicle
- `DELETE /api/Vehicle/{id}` — Delete vehicle by ID

- `GET /api/Trip` — List all trips
- `GET /api/Trip/{id}` — Get trip by ID
- `POST /api/Trip` — Create new trip
- `PUT /api/Trip/{id}` — Update existing trip
- `DELETE /api/Trip/{id}` — Delete trip by ID

- `GET /api/FareBand` — List all fare bands ordered by distance limit
- `GET /api/FareBand/{id}` — Get fare band by ID
- `POST /api/FareBand` — Create new fare band
- `PUT /api/FareBand/{id}` — Update existing fare band
- `DELETE /api/FareBand/{id}` — Delete fare band by ID
  
- `POST /api/fare/calculate` — Calculate fare based on distance

## Validation
- All request DTOs use Data Annotations and FluentValidation for input validation
- Invalid requests return `400 Bad Request` with error details

## Cross-Cutting Concerns
- **Exception Handling:** Global middleware (ExceptionHandlingMiddleware) catches and logs exceptions
- **Logging:** All unhandled exceptions are logged
- **Validation:** Data Annotations and FluentValidation
- **Caching:** In-memory cache for fare bands
- **Swagger:** API documentation with XML comments
- **API Rate Limiting**: Header-based client partitioning with configurable request limits
- **Dependency Injection**: Scoped service registration for repositories and services
- **Object Mapping**: AutoMapper for mapping between DTOs and domain models
- **Repository Pattern**: Generic implementation for data access abstraction
- **CI/CD Pipeline**: Continuous integration and delivery using GitHub Actions
- **Containerization**: Docker support for consistent deployment environments
- **Unit Testing**: xUnit tests with Moq for comprehensive service, repository and controller testing, ensuring reliability through automated regression testing

## Authors
- Neven Hossam

---
For more details, see Swagger UI and XML comments in the codebase.
