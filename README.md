# E-Shop API

This project is a simple e-commerce back-end API built with .NET 8, demonstrating a clean architecture approach with CQRS. It includes features for product management, user authentication, and shopping cart functionality.

## Core Concepts & Technologies

This solution is built adhering to modern software architecture principles and technologies:

- **Environment**: .NET 8
- **Database**: Designed for PostgreSQL, compatible with MySQL 8.
- **Architecture**: Clean Architecture, separating concerns into distinct layers (Domain, Application, Infrastructure, Presentation).
- **Design Patterns**:
    - **Command Query Responsibility Segregation (CQRS)**: Explicitly separates read and write operations for better performance, scalability, and security.
    - **Repository Pattern**: Abstracts data access logic.
- **Data Access**:
    - **Entity Framework Core**: Used as the Object-Relational Mapper (ORM).
    - **Fluent API**: Entity configurations are defined in code for maximum control, avoiding Data Annotations.
- **Validation**:
    - **FluentValidation**: Used for validating incoming commands and DTOs, keeping validation logic clean and separate from models.
- **Object Mapping**: DTOs and projections are used for mapping data, avoiding reflection-based auto-mappers.
- **API**: RESTful API built with ASP.NET Core.

## Features

- **Product Management**:
    - Inventory Managers can create, update, and delete product categories.
    - Inventory Managers can add products with a title, description, and images.
    - Each product can have multiple options/sizes, each with a different price.
- **Authentication & Authorization**:
    - Role-based access control is implemented using ASP.NET Core Identity.
    - **Roles**:
        - **InventoryManager**: Can manage products and categories.
        - **SalesManagers**: Can view orders and customer data (functionality to be extended).
        - **Client**: Can register, view products, and manage their shopping cart.
- **Shopping Cart**:
    - Clients can add, view, and update items in their cart.
    - Cart data is persisted using a Redis cache for high performance.
- **Database Seeding**: Initial roles and users are seeded on application startup.

## Prerequisites

To build and run this project, you will need:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) or [MySQL](https://www.mysql.com/downloads/)
- A Redis instance (via Upstash)
- Visual Studio 2022 

## Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/your-repository-name.git](https://github.com/your-username/your-repository-name.git)
    cd your-repository-name
    ```

2.  **Configure Database Connection:**
    Open `EShop.API/appsettings.Development.json` and update the `DefaultConnection` string with your database credentials.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=EshopTask_db;Username=postgres;Password=your_password"
    }
    ```

3.  **Configure Redis Cache:**
    Open `Infrastructure/InfrastructureAssembly.cs` and update the Redis `ConfigurationOptions` with your connection details. **Note:** It is highly recommended to move these settings to `appsettings.json` and use User Secrets for sensitive data.

4.  **Apply Database Migrations:**
    Run the following command from the root of the `EShop.API` project to create and update your database schema:
    ```bash
    dotnet ef database update
    ```

5.  **Run the Application:**
    You can run the project from your IDE or by using the .NET CLI:
    ```bash
    dotnet run --project EShop.API/EShop.API.csproj
    ```
    The API will be available at `https://localhost:7130` and `http://localhost:5014`. The Swagger UI can be accessed at `https://localhost:7130/swagger`.

## API Endpoints Overview

- `POST /api/ProductCommand` - Add a new product (Requires InventoryManager role).
- `PUT /api/ProductCommand/{id}` - Update an existing product (Requires InventoryManager role).
- `DELETE /api/ProductCommand/{id}` - Delete a product (Requires InventoryManager role).
- `GET /api/ProductQuery` - Get a paginated list of all products.
- `GET /api/ProductQuery/{id}` - Get a single product by its ID.
- `GET /api/Cart/{id}` - Get a user's cart (Requires Client role).
- `POST /api/Cart` - Create or update a user's cart (Requires Client role).
- `DELETE /api/Cart/{id}` - Delete a user's cart (Requires Client role).
- `POST /register` - Register a new user.
- `POST /login` - Login to get a JWT token.

## Project Structure

The solution follows the principles of Clean Architecture:

- **`Domain`**: Contains all enterprise-wide logic, including entities, value objects, and repository interfaces. It has no dependencies on other layers.
- **`Application`**: Contains application-specific logic. It implements the CQRS pattern with commands, queries, handlers, and DTOs. It depends only on the Domain layer.
- **`Infrastructure`**: Contains external concerns like data access (EF Core, Repositories), caching (Redis), and other third-party integrations. It depends on the Application layer.
- **`Presentation` (within `EShop.API`)**: The entry point of the application, containing the API controllers. It depends on the Application layer.
