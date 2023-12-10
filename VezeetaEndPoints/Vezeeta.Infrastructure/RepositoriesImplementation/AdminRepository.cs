using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;

namespace Vezeeta.Infrastructure.RepositoriesImplementation
{
    public  class AdminRepository : IAdminRepository
    {
        private readonly VezeetaContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public AdminRepository(VezeetaContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public int NumOfDoctors()
        {
            return _context.Doctors.Count<Doctor>();
        }

        public int NumOfPatients()
        {
            return _context.AllUsers.Count<User>(d=>d.type == UserType.patient);
        }

        public dynamic NumOfRequests()
        {
            int totalRequests = _context.Bookings.Count();
            int pendingRequests = _context.Bookings.Count(d => d.BookingStatus == Core.Models.Status.pending);
            int canceledRequests = _context.Bookings.Count(d => d.BookingStatus == Core.Models.Status.canceled);
            int completedRequests = _context.Bookings.Count(d => d.BookingStatus == Core.Models.Status.completed);
            return new { totalRequests, pendingRequests, canceledRequests, completedRequests };


        }

        public dynamic Top10Doctors()
        {
            var result = _context.Bookings
      .Join(
          _context.Doctors,
          booking => booking.DoctorID,
          doctor => doctor.doctorid,
          (booking, doctor) => new { Booking = booking, Doctor = doctor }
      )
      .Join(
          _context.Users,
          joined => joined.Doctor.userId,
          user => user.userId,
          (joined, user) => new { Joined = joined, User = user }
      )
      .GroupBy(
          joinedUser => joinedUser.User.fname + " " + joinedUser.User.lname,
          joinedUser => joinedUser.Joined.Booking.BookingID,
          (fullname, count) => new
          {
              FullName = fullname,
              Requests = count.Count()
          }
      ).OrderByDescending(result => result.Requests)
  .Take(10); ;

            return result;
        }

        public dynamic Top5Specializations()
        {
           
                var result = _context.Bookings
                .Join(
                    _context.Doctors,
                    booking => booking.DoctorID,
                    doctor => doctor.doctorid,
                    (booking, doctor) => new { Booking = booking, Doctor = doctor }
                )
                .Join(
                    _context.Specializations,
                    joined => joined.Doctor.specializationID,
                    specialization => specialization.specializationID,
                    (joined, specialization) => new { Joined = joined, Specialization = specialization }
                )
                .GroupBy(
                    joinedSpecialization => joinedSpecialization.Specialization.specializationName,
                    joinedSpecialization => joinedSpecialization.Joined.Booking.BookingID,
                    (specializationName, count) => new
                    {
                        SpecializationName = specializationName,
                        Count = count.Count()
                    }
                ).OrderByDescending(result => result.Count) // Order by request count in descending order
                .Take(5); ;

            return result;

            }

        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
            var doctors = _context.Doctors
     .Select(doctor => new
     {
         doctor.doctorid,
         doctor.price,

         specialization = _context.Specializations
                     .Where(s => s.specializationID == doctor.specializationID)
                     .Select(s => s.specializationName)
                     .FirstOrDefault(),
         doctorAppointments = _context.Appointments
                     .Where(a => a.doctorID == doctor.doctorid)
         .Join(
                         _context.TimeSlots,
                         appointment => appointment.Id,
                         timeSlot => timeSlot.AppointmentID,
                         (appointment, timeSlot) => new
                         {
                             day = appointment.day.ToString(),
                             timeSlot.Time
                         })
                     .ToList(),
         doctor.fname,
         doctor.lname,
         doctor.email,
         doctor.image,
         doctor.phoneNumber,
         gender = doctor.gender.ToString()
     })
             .ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchedDocs = from d in doctors
                                   where d.fname.Contains(search) ||
                                   d.email.Contains(search) ||
                                   d.specialization.Contains(search) ||
                                   d.gender.Contains(search) ||
                                   d.phoneNumber.Contains(search) ||
                                   d.doctorAppointments.Select(a => a.day).Contains(search)
                                   select d;

                var paginationDoc = searchedDocs.Skip((page - 1) * pageSize).Take(pageSize);
                if (searchedDocs != null)
                {
                    return paginationDoc;
                }
                else
                    return doctors.Skip((page - 1) * pageSize).Take(pageSize);


            }
            else
                return doctors.Skip((page - 1) * pageSize).Take(pageSize);


        }

        public dynamic GetDoctorById (int doctorId)
        {
            var doctor = _context.Doctors.Where(d => d.doctorid == doctorId).FirstOrDefault();

            if (doctor != null)
            {
                var specializationOfDoctor = _context.Specializations.Where(s => s.specializationID == doctor.specializationID).FirstOrDefault();
                return new
                {
                    doctor.image,
                    fullname = (doctor.fname + " " + doctor.lname),
                    doctor.email,
                    doctor.phoneNumber,
                    specializationOfDoctor.specializationName,
                    doctor.gender
                };
            }
            else
               return HttpStatusCode.NotFound;

        }
        private string generatePassword()
        {
            Random random = new Random();
            const string letters = "abcdefghijklmnopqrstuvwxyz";
            const string UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string specialChars = "!@";
            const string numbers = "0123456789";
            StringBuilder password = new StringBuilder();

            // Generate 6 random letters (mix of lowercase and uppercase)
            for (int i = 0; i < 6; i++)
            {
                int letterIndex = random.Next(letters.Length);
                password.Append(letters[letterIndex]);
            }

            for (int i = 0; i < 6; i++)
            {
                int letterIndex = random.Next(UpperLetters.Length);
                password.Append(UpperLetters[letterIndex]);
            }

            // Add a special character
            int specialCharIndex = random.Next(specialChars.Length);
            password.Append(specialChars[specialCharIndex]);

            // Add a number
            int numberIndex = random.Next(numbers.Length);
            password.Append(numbers[numberIndex]);

            // Generate the rest of the characters (total 12 characters)
            for (int i = 0; i < 4; i++) // Remaining 4 characters to make it 12 in total
            {
                int index = random.Next(letters.Length + specialChars.Length + numbers.Length);
                if (index < letters.Length)
                {
                    password.Append(letters[index]);
                }
                else if (index < letters.Length + specialChars.Length)
                {
                    password.Append(specialChars[index - letters.Length]);
                }
                else
                {
                    password.Append(numbers[index - (letters.Length + specialChars.Length)]);
                }
            }

            return password.ToString();
        }

        public async Task<string> AddDoctor(AddDoctorDTO doctor)
        {
            if (doctor != null)
            {
                User doc = new User()
                {
                    UserName = doctor.email,
                                     Email = doctor.email,
                    fname = doctor.fname,
                    lname = doctor.lname,
                    email = doctor.email,
                    dateOfBirth = doctor.dateOfBirth,
                    image = doctor.image,
                    phoneNumber = doctor.phone,
                    gender = doctor.gender,
                    password = generatePassword(),
                   
                    type = UserType.doctor
                };
                var result = await _userManager.CreateAsync(doc, doc.password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(doc, "Doctor");
                    _context.Users.Add(doc);

                    _context.Database.ExecuteSqlRaw($"insert into Doctors values (\'{doc.Id}\',1,{doctor.price},{doctor.specializationID});");

                    return doc.Id;
                }
                else
                    return "";


            }
            else
                return null;

        }
        //needs testing
        public HttpStatusCode EditDoctor(string doctorID, AddDoctorDTO doctor)
        {
            var findDoctor = _context.Doctors.FirstOrDefault(d => d.Id == doctorID);
            if (findDoctor != null)
            {
                User doc = new User()
                {
                    fname = doctor.fname,
                    lname = doctor.lname,
                    email = doctor.email,
                    dateOfBirth = doctor.dateOfBirth,
                    image = doctor.image,
                    phoneNumber = doctor.phone,
                    gender = doctor.gender,
                    type = UserType.doctor
                };
                _context.Set<User>().Update(doc);
                _context.SaveChanges();

                _context.Database.ExecuteSqlRaw($"update Doctors set price ={doctor.price} and specializationID = {doctor.specializationID} where doctorid= {doctorID} );");

                _context.SaveChanges();
                return HttpStatusCode.OK;
            }
            else
             return  HttpStatusCode.NotFound;

        }

        public async  Task<HttpStatusCode> DeleteDoctor(string doctorID)
        {
            var findDoctor = _context.Doctors.FirstOrDefault(d => d.Id == doctorID);
            var UserDoctor = _context.Users.FirstOrDefault(u => u.Id == doctorID);
            if (findDoctor != null && UserDoctor != null)
            {

                _context.Doctors.Remove(findDoctor);
                _context.Users.Remove(UserDoctor);
                await _userManager.DeleteAsync(UserDoctor);
                _context.SaveChanges();
                return HttpStatusCode.OK;
            }
            else return HttpStatusCode.NotFound;

        }

        public dynamic GetallPatients(int page, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public dynamic getPatientByID(int patientId)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode AddDiscount(DiscountDTO discountInfo)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode DeleteDiscount(int discountID)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode DeactivateDiscount(int discountID)
        {
            throw new NotImplementedException();
        }
    }
}
