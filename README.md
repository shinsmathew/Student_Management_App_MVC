Student Management App - MVC

A comprehensive student management system built with ASP.NET Core 8 MVC, implementing a layered architecture, cookie-based authentication, Redis caching, and robust validation mechanisms for managing student and user data.

✨ Features
-----------------
🔐 Authentication & Authorization

Cookie-based authentication
Role-based access control (Admin / Student)

👩‍🎓 Student Management

Full CRUD operations for student records
Validation for student age (5-25 years), contact details, and more

👥 User Management

Registration with hashed password storage
Secure login / logout workflows

⚡ Redis Caching

Improved performance through caching of student lists and details

🛡️ Validation & Error Handling

FluentValidation for data integrity
Global exception handling middleware

📦 Clean Structure & Mapping

DTO-to-entity mapping via AutoMapper
Separation of concerns using repositories, services, controllers

📘 UI

Role-based dashboards for Admin and Student
Bootstrap-styled, responsive views


🛠️ Tech Stack

Framework: ASP.NET Core 8 MVC
Language: C#
Database: SQL Server
Caching: Redis
Authentication: Cookie-based auth
ORM: Entity Framework Core
Validation: FluentValidation
Mapping: AutoMapper
UI: Bootstrap 5

✅ Prerequisites

Before running the project, ensure you have:
.NET 8 SDK
SQL Server (or SQL Express)
Redis server (e.g. Redis for Windows or Docker)
Visual Studio 2022+ or another IDE
