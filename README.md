# Algoriza Internship Back-End Developer Track : Vezeeta Website (Endpoints)
## Student Information:
 Student Code: BE112/ 2023BE112
 <br>
 Student Name: Salma Magdy Ishak
 # 
 ## Technologies Used: 
[![SQL](https://img.shields.io/badge/SQL-Used-orange)](https://www.microsoft.com/en-us/sql-server)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Used-blue)](https://www.microsoft.com/en-us/sql-server)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Used-green)](https://dotnet.microsoft.com/apps/aspnet)
[![ASP.NET](https://img.shields.io/badge/ASP.NET-Used-blueviolet)](https://dotnet.microsoft.com/apps/aspnet)
[![EF Core](https://img.shields.io/badge/EF%20Core-Used-success)](https://docs.microsoft.com/en-us/ef/core/)
[![ASP.NET Core Web API](https://img.shields.io/badge/ASP.NET%20Core%20Web%20API-Used-yellow)](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
[![JWT Authentication in ASP.NET Core](https://img.shields.io/badge/JWT%20Authentication-Used-green)](https://jwt.io/introduction/)



 ## Project Overview:
#### This project aims to provide hands-on practice on ASP.NET Core, SQL Server, ASP.NET Core APIs, Identity Core, JWT Authentication and Authorizations,Entity Framework and Onion Architecture.
#
## Project Logical Entities Consists of: 
- Admin (User Class)
- Doctors (Doctor Class Extension of User Class)
- Patient (User Class)
## User Authentication: 
User authentication and Authorization is done using <b>JWT Token</b>, with roles based on our 3 project entities.
Role, for all entities types, is determined with user login with his given creditionals. He is given the access to only his authorized endpoints.
Admin Data Has been seeded into the database as the App has one admin. 
```
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireAuthenticatedUser(); 
        policy.RequireRole("Admin"); 
    });

    options.AddPolicy("Doctor", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Doctor");
    });

    options.AddPolicy("Patient", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Patient");
    });
});
 ```

## Onion Architecture, it consists of four layers:
<b> <i> --> Main Project : VezeetaEndPoints </b> </i>
- Core Layer (Vezeeta.Core)
  --
  - Models
    - Appointment
    - Booking
    - Doctor
    - User
    - Discount
    - Specalization
    - Timeslot
  - DTOs
    - AddAppointmentDTO
    - AddDoctorDTO
    - DiscountDTO
    - PatientDTO
  - Repositories
    - IAdminRepository
    - IPatientRepository
    - IDoctorRepository
  
- Infrastructure Layer (Vezeeta.Infrastructure)
  --
  - Dbcontexts
    - VezeetaContext
  - Repositories Implementations
    - AdminRepository
    - PatientRepository
    - DoctorRepository
  - Migrations
- Services Layer (Vezeeta.Services)
  --
  - Interfaces
    - IAdminServices
    - IPatientServices
    - IDoctorServices
    - IEmailService
  - Services
    - AdminServices
    - DoctorServices
    - PatientServices
    - EmailServices
- Presentation Layer (Vezeeta.Presentation.API)
  --
  - API Endpoints Controller
    - AdminController
    - DoctorController
    - PatientController
    - UserController
  - Program.cs 
#
## SQL Server Database Diagram: 
(created inside SSMS)
![algoriza updated DB](https://github.com/Salmaishak/algoriza-internship-2023BE112/assets/96662980/91cb8a2a-10f6-4ffd-8783-36aaa43d5a27)
## Email Service 
- Service Used : SendGrid API
- Example of Email: <br>
<img src="https://github.com/Salmaishak/algoriza-internship-BE112/assets/96662980/dc9972b5-64d7-4ce0-bd1b-06a66f75d207" alt="Image" width="800"></img>
## Inner Code Enums: 
- User Type
  - Admin
  - Doctor
  - Patient
- Booking Status
  - Pending
  - Completed
  - Canceled
- Gender
  - Female
  - Male
- Day Of Week
  - Saturday
  - Sunday
  - Etc...
- Discount Activity
  - Active
  - Deactive
- Discount Type
  - Percentage
  - Value

 
