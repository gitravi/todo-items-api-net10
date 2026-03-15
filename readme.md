# TodoApi

A simple ASP.NET Core Web API for managing todo items.

## Implementation Details

- Built with ASP.NET Core 8.0
- Follows RESTful principles
- Uses Entity Framework Core for data access
- Supports CRUD operations for todo items
- Includes Swagger for API documentation

## NuGet Packages Used

- `Microsoft.AspNetCore.OpenApi`  
    For Swagger/OpenAPI documentation.

- `Microsoft.EntityFrameworkCore`  
    Core Entity Framework functionality.

- `Microsoft.EntityFrameworkCore.SqlServer`  
    SQL Server database provider.

- `Microsoft.EntityFrameworkCore.Tools`  
    EF Core command-line tools.

- `Swashbuckle.AspNetCore`  
    Swagger UI integration.

## Getting Started

1. Clone the repository.
2. Run `dotnet restore` to install dependencies.
3. Update `appsettings.json` with your database connection string.
4. Run database migrations:  
     `dotnet ef database update`
5. Start the API:  
     `dotnet run`

## API Endpoints

- `GET /api/todo` - List all todo items
- `GET /api/todo/{id}` - Get a specific todo item
- `POST /api/todo` - Create a new todo item
- `PUT /api/todo/{id}` - Update a todo item
- `DELETE /api/todo/{id}` - Delete a todo item

