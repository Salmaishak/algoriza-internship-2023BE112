# Algoriza Internship Back-End Developer Track : Vezeeta Website (Endpoints)
## Student Information:
 Student Code: BE112/ 2023BE112
 <br>
 Student Name: Salma Magdy Ishak
 # 
 [![SQL](https://img.shields.io/badge/SQL-Used-orange)](https://www.microsoft.com/en-us/sql-server)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Used-blue)](https://www.microsoft.com/en-us/sql-server)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Used-green)](https://dotnet.microsoft.com/apps/aspnet)
[![ASP.NET](https://img.shields.io/badge/ASP.NET-Used-blueviolet)](https://dotnet.microsoft.com/apps/aspnet)
[![EF Core](https://img.shields.io/badge/EF%20Core-Used-success)](https://docs.microsoft.com/en-us/ef/core/)
[![ASP.NET Core Web API](https://img.shields.io/badge/ASP.NET%20Core%20Web%20API-Used-yellow)](https://docs.microsoft.com/en-us/aspnet/core/web-api/)

 ## Project Overview:
#### This project aims to provide hands-on practice on ASP.NET Core, SQL Server, ASP.NET Core APIs, Entity Framework and Onion Architecture.
#
### Project Entities Consists of: 
- Admin
- Doctors
- Patient

### The project follows Onion Architecture, it consists of four layers:
Main Project : Vezeeta.Core
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
#
## SQL Server Database Diagram: 
![algo](https://github.com/Salmaishak/algoriza-internship-BE112/assets/96662980/f38796cd-343f-47c3-a1c9-99538284e009)
## Email Service 
- Service Used : SendGrid API
- Example of Email:
![image](https://github.com/Salmaishak/algoriza-internship-BE112/assets/96662980/e071787f-76d7-4a3d-9845-1a591fd323af)

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

 
