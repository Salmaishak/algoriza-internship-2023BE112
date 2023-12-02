# Algoriza Internship Back-End Developer Track : Vezeeta Website (Endpoints)
## Student Information:
 Student Code: BE112/ 2023BE112
 <br>
 Student Name: Salma Magdy Ishak
 # 
 ## Project Overview:
#### This project aims to provide hands-on practice on ASP.NET Core, SQL Server, ASP.NET Core APIs, Entity Framework and Onion Architecture.
#
### Project Entities Consists of: 
- Admin
- Doctors
- Patient

### The project consists of four layers:
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
  - Services
    - AdminServices
    - DoctorServices
    - PatientServices
- Presentation Layer (Vezeeta.Presentation.API)
  --
  - API Endpoints Controller
    - AdminController
    - DoctorController
    - PatientController
#
## SQL Server Database Diagram: 
![algo](https://github.com/Salmaishak/algoriza-internship-BE112/assets/96662980/f38796cd-343f-47c3-a1c9-99538284e009)

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

 
