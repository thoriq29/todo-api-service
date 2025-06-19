
# To-Do List API .NET 8

Welcome to the To-Do List API! This is a backend service built with .NET 8, using a Controller-based architecture and Entity Framework Core to provide full functionality for managing checklists and items. This API is secured using JWT (JSON Web Token) authentication.

## Key Features

- **User Management**: Register and Login.
- **JWT Authentication**: Secure endpoints using Bearer tokens.
- **Checklist Management**: Create, retrieve all, and delete checklists.
- **Item Management**: Create, view, toggle status, rename, and delete items within a checklist.
- **Clean Architecture**: Utilizes Repository and Service Pattern for logic separation.
- **Database**: Uses MySQL with Entity Framework Core.
- **API Documentation**: Integrated with Swagger (OpenAPI) for easy testing.

## 1. Prerequisites

Before you begin, make sure you have the following installed:

- .NET 8 SDK: [Download here](https://dotnet.microsoft.com/)
- MySQL Server: [Download here](https://www.mysql.com/) (or use Docker, XAMPP, etc.)
- IDE or Code Editor: Visual Studio 2022, JetBrains Rider, or Visual Studio Code.
- Database Client Tool: MySQL Workbench, DBeaver, or any other database manager.

## 2. Installation and Setup

Follow the steps below to run this project in your local environment.

### a. Get the Code

Clone this repository or download the project files if you already have them.

### b. Configure the Application

Open the `appsettings.json` file in the root directory of the project.

Update the `ConnectionStrings` section to match your MySQL database configuration (user, password, and server). You do not need to create the database manually; EF Core will handle that for you.

Change `Jwt:Key` to a long, unique, and secure secret string.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=todo_db_dotnet;user=root;password=your_password"
  },
  "Jwt": {
    "Key": "REPLACE_WITH_YOUR_SUPER_SECRET_KEY",
    "Issuer": "TodoApi",
    "Audience": "TodoApiClient"
  }
}
```

### c. Install Dependencies

Open a terminal or command prompt in the root project directory and run the following command to install all required NuGet packages:

```bash
dotnet restore
```

If any packages are missing, you can install them manually:

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package BCrypt.Net-Next
```

### d. Database Setup (EF Core Migrations)

Entity Framework Core will automatically create the database and tables for you.

**Create Migration**:

```bash
dotnet ef migrations add InitialCreate
```

**Apply Migration**:

```bash
dotnet ef database update
```

### Alternative: Manual SQL Setup

If you prefer not to use EF Core migrations, you can manually create the database (`CREATE DATABASE todo_db_dotnet;`) and then run the following SQL scripts:

```sql
CREATE TABLE Users (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Username NVARCHAR(255) NOT NULL UNIQUE,
  Email NVARCHAR(255) NOT NULL UNIQUE,
  PasswordHash NVARCHAR(255) NOT NULL,
  CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Checklists (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name NVARCHAR(255) NOT NULL,
  UserId INT NOT NULL,
  CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE Items (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Name NVARCHAR(255) NOT NULL,
  Status BOOLEAN DEFAULT FALSE,
  ChecklistId INT NOT NULL,
  CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (ChecklistId) REFERENCES Checklists(Id) ON DELETE CASCADE
);
```

### e. Run the Application

After completing all setup, run the application using:

```bash
dotnet run
```

The application will start and is usually accessible at `https://localhost:7xxx` or `http://localhost:5xxx`. The exact URL will be displayed in the terminal.

## 3. How to Use the API (with Swagger)

Once the application is running, open your browser and navigate to the `/swagger` endpoint (e.g., https://localhost:7022/swagger).

### Step 1: Register a User

- Open the `POST /register` endpoint.
- Click "Try it out".
- Fill in `username`, `email`, and `password`.
- Click "Execute".

### Step 2: Login and Get Token

- Open the `POST /login` endpoint.
- Click "Try it out".
- Enter the registered `username` and `password`.
- Click "Execute".
- Copy the token from the response.

```json
{
  "data": {
    "message": "Login successful!",
    "token": "eyJhbGciOiJI..." // <--- COPY THIS TOKEN
  },
  "status": 200,
  "error": null
}
```

### Step 3: Authorize Swagger

- Click the "Authorize" button at the top-right corner of the Swagger page.
- Paste the token with the format: `{your_token}`
- Click "Authorize", then "Close".

### Step 4: Test Protected Endpoints

You are now authenticated. Try other endpoints such as `GET /checklist`, `POST /checklist`, etc. Swagger will automatically include your token in every request.
