# LoanApi

# Loan API Project

## Project Overview

The **Loan API** is a RESTful Web API developed using **ASP.NET Core**. It manages user registration, loan applications, and role-based authorization for users and accountants.

---

## Features

### General Features

- **User Registration and Authorization**
- **Role-Based Authorization**: Roles include `Accountant` and `User`.
- **JWT Authentication** for secure access to endpoints.
- **Loan Management**:
  - Add, update, delete, and view loan data.
  - Users can only manage their loans.
  - Accountants can view, update, and delete all loans.
  - Accountants can block users to restrict loan creation.
- **Error Handling**: Global exception handling using middleware.
- **Validation**: Input validation with **FluentValidation**.
- **Logging**: Application logging using **Serilog**.
- **Unit Testing**: Covered with xUnit and Moq.

---

## API Endpoints

### User Authentication

- **POST /api/User/register**
  - Registers a new user.
  - Requires: `firstName`, `lastName`, `username`, `email`, `age`, `salary`, and `password`.
- **POST /api/User/login**
  - Logs in a user and generates a JWT token.

### Loan Management

#### User Role

- **POST /api/Loan**
  - Adds a new loan.
  - Requires a valid token and loan data.
  - Loan statuses default to `Processing`.
- **GET /api/Loan**
  - Retrieves all loans for the authenticated user.
- **GET /api/Loan/{loanId}**
  - Retrieves specific loan data for the authenticated user.
- **PUT /api/Loan/{loanId}**
  - Updates an existing loan.
  - Allowed only if the loan status is `Processing`.
- **DELETE /api/Loan/{loanId}**
  - Deletes a loan.
  - Allowed only if the loan status is `Processing`.

#### Accountant Role

- **GET /api/Accountant/loans**
  - Retrieves all loans in the system.
- **PUT /api/Accountant/blockUser/{userId}**
  - Blocks a user from creating loans.
- **DELETE /api/Accountant/loan/{loanId}**
  - Deletes any loan, regardless of status.

---

## Technologies Used

- **.NET 7**: ASP.NET Core Web API
- **Entity Framework Core**: Database interactions
- **SQL Server**: Database
- **JWT**: Authentication
- **FluentValidation**: Request validation
- **Serilog**: Logging
- **xUnit and Moq**: Unit testing

---

## Project Structure

```
LoanSolution/
├── LoanApi/
│   ├── Controllers/        # API Controllers
│   ├── Data/               # DbContext and Migrations
│   ├── Middleware/         # Exception handling middleware
│   ├── Models/             # DTOs and Entities
│   ├── Repositories/       # Repository interfaces and implementations
│   ├── Services/           # Service interfaces and implementations
│   ├── Validators/         # FluentValidation validators
│   ├── appsettings.json    # Configuration (JWT, Database, etc.)
│   └── Program.cs          # Entry point
└── LoanApi.Tests/          # Unit tests
```

---

## Installation and Setup

### Prerequisites

- **.NET 7 SDK**
- **SQL Server**
- **Postman** (or any API testing tool)

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/LoanApi.git
   ```
2. Navigate to the project folder:
   ```bash
   cd LoanSolution/LoanApi
   ```
3. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=LoanApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
4. Run database migrations:
   ```bash
   dotnet ef database update
   ```
5. Start the application:
   ```bash
   dotnet run
   ```
6. Access Swagger for API testing:
   - Open `https://localhost:5001/swagger` (or the relevant port).

---

## Testing

### Running Tests

Unit tests are written using **xUnit** and **Moq**.

1. Navigate to the test project:
   ```bash
   cd LoanSolution/LoanApi.Tests
   ```
2. Run the tests:
   ```bash
   dotnet test
   ```

---

## Usage

### Postman Configuration

- Set the **Bearer Token** in the Postman collection for authorization.
- Use the endpoints listed above to test the application.

### Generating JWT

1. Register a user via `POST /api/User/register`.
2. Log in via `POST /api/User/login` to get a token.
3. Use the token for authorization in subsequent requests.

---

## Contributions

Feel free to fork the project and submit pull requests for improvements.

---

## License

This project is licensed under the MIT License.
