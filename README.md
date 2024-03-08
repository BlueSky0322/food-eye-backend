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
2. Ensure all the dependencies are installed.
![Screenshot](https://github.com/BlueSky0322/food-eye-backend/assets/60435524/0a01dbd8-4ad7-4c88-9ed3-2cb747e3e3c8)
3. If not, install via NuGet Package Manager:
<img width="300" alt="Screenshot 2024-03-08 at 12 44 26 PM" src="https://github.com/BlueSky0322/food-eye-backend/assets/60435524/9cb8864f-48ea-4f7f-bbfd-377f116ba220">
4. 

###Database instance
1. From VS 2022, go to `View > SQL Server Object Explorer` and open the SQL Server Object Explorer.
2. Locate the local database connection.
3. View the instance properties. From there, find the Connection String parameter and copy the entire string.
4. Open `appsettings.json`, replace the connection string `FoodEyeContextConnection` with the copied one, replace the Initial Catalog parameter with `FoodEyeDB` (from “master” change to “FoodEyeDB”). 
