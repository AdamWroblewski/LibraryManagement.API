# Library Management System API

A RESTful Web API for library management built with **.NET 8** following **Clean Architecture** principles. The system handles book catalog management, user authentication, reservation and loan tracking, and book reviews.

**Live Demo & Documentation:** [Swagger UI](https://librarymanagementapi-cqh6g6dhavhzcyg6.polandcentral-01.azurewebsites.net/swagger/index.html)

## Demo Credentials

You can test the live endpoints directly on [Swagger UI](https://librarymanagementapi-cqh6g6dhavhzcyg6.polandcentral-01.azurewebsites.net/swagger/index.html) using the following pre-seeded test accounts:

| Role | Email | Password | Access Rights |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@library.demo` | `Admin123!` | System administration & staff management (e.g., employee account creation) |
| **Employee** | `employee@library.demo` | `Employee123!` | Catalog management (CRUD), direct book checkout & return processing |
| **User** | `user@library.demo` | `User123!` | Public catalog browsing, self-service book reservations & reviews |

> **Note:** Use the `/api/auth/login` endpoint to retrieve a JWT token, then paste it into the Swagger **Authorize** button (`Bearer <token>`) to test protected routes.
---

## Technical Features

* **Architecture:** Structured following Clean Architecture separation across Domain, Application, Infrastructure, and API layers.
* **CQRS Pattern:** Implemented using **MediatR** for decoupled command and query processing.
* **Authentication & Authorization:** Secure JWT Bearer authentication with ASP.NET Core Identity. Supports role-based access control (`Admin`, `Employee`, `User`).
* **Database & ORM:** SQL Server managed via Entity Framework Core 8 with automated code-first migrations.
* **Validation:** Pipeline validation using **FluentValidation** prior to handling requests.
* **Background Processing:** Hosted background services for automated cleanup tasks (e.g., handling expired reservations).
* **Logging & Error Handling:** Structured logging with **Serilog** and centralized exception management returning RFC 7807 Problem Details.
* **CI/CD Pipeline:** Configured with Azure Pipelines for automated building, publishing, migration execution, database seeding, and Azure Web App deployment.

---

## Project Structure

```text
LibraryManagement/
├── LibraryManagement.API            # Presentation layer, Controllers, Middleware, Pipeline configuration
├── LibraryManagement.Application    # Use cases, Commands/Queries (MediatR), DTOs, Mapping profiles, Validation
├── LibraryManagement.Domain         # Core domain entities, Enums, Interfaces, Domain exceptions
└── LibraryManagement.Infrastructure # EF Core DbContext, Identity, Repositories, Background Services
```

---

## Core Domain & Features

* **Auth Management:** User registration, JWT login authentication, and password changes.
* **Book Catalog:** Full CRUD management for books (restricted to employees) alongside public paged catalog browsing.
* **Book Loans & Reservations:**
  * Self-service book reservations by users (with automatic expiration after 72 hold hours).
  * Direct issue and return actions managed by employees (with a standard 28-day checkout period).
  * Comprehensive loan status state tracking (`Reserved`, `Active`, `Returned`, `Overdue`, `Expired`, `Cancelled`).
* **Reviews & Ratings:** User reviews with 1–5 star rating support per book.

---

## Getting Started

### Live API

You can explore and test all API endpoints directly without local installation using the deployed Swagger UI:
👉 **[Open Live Swagger UI](https://librarymanagementapi-cqh6g6dhavhzcyg6.polandcentral-01.azurewebsites.net/swagger/index.html)**

### Local Setup

#### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* SQL Server or LocalDB instance

#### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/AdamWroblewski/LibraryManagement.API
   cd LibraryManagement.API/
   ```

2. **Configure Database Connection:**
   Update the `DefaultConnection` string in `LibraryManagement.API/appsettings.Development.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Run the Application:**
   ```bash
   dotnet run --project LibraryManagement.API
   ```
   *Automatic migrations and seed data execution run on launch under the `Development` environment.*

4. **Explore the Local API:**
   Navigate to [http://localhost:5074/](http://localhost:5074/) or [https://localhost:7097/](https://localhost:7097/) in your browser.
