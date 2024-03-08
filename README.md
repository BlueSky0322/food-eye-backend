# Introduction
Backend API Endpoint for FoodEye built using ASP.NET Core Web API 

# Technologies Used
- NuGet Package Manager for dependencies/libraries/packages
- ASP.NET Core Web API (MVC Framework)

# Getting Started
**Context:** The complete project is a mobile application that consists of a frontend, backend, and database. 
- [Frontend](https://github.com/BlueSky0322/food-eye-frontend) is developed using Flutter framework, through VS Code IDE.
- Backend is developed using ASP .NET Core framework, through Visual Studio 2022.
- Database is local MSSQL instance, accessed from Visual Studio 2022 (SQL Server Object Explorer). 

# Running The Project
### Backend code
1. Clone the repo:
```
git clone repo-web-url/ssh-key
```
2. Ensure all the dependencies are installed
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 7.0.3 Microsoft.AspNetCore.OpenApi --version 7.0.5 Microsoft.EntityFrameworkCore --version 7.0.3 Microsoft.EntityFrameworkCore.Design --version 7.0.3 Microsoft.EntityFrameworkCore.SqlServer --version 7.0.3 Microsoft.EntityFrameworkCore.Tools --version 7.0.3 Swashbuckle.AspNetCore --version 6.4.0
```
Alternatively, you can install them via NuGet Package Manager, here's the list:
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (7.0.3)
- Microsoft.AspNetCore.OpenApi (7.0.5)
- Microsoft.EntityFrameworkCore (7.0.3)
- Microsoft.EntityFrameworkCore.Design (7.0.3)
- Microsoft.EntityFrameworkCore.SqlServer (7.0.3)
- Microsoft.EntityFrameworkCore.Tools (7.0.3)
- Swashbuckle.AspNetCore (6.4.0)

### Database instance
1. From VS 2022, go to `View > SQL Server Object Explorer` and open the SQL Server Object Explorer.
2. Locate the local database connection.
3. View the instance properties. From there, find the Connection String parameter and copy the entire string.
4. Open `appsettings.json`, replace the connection string `FoodEyeContextConnection` with the copied one, replace the Initial Catalog parameter with `FoodEyeDB` (from “master” change to “FoodEyeDB”). 
