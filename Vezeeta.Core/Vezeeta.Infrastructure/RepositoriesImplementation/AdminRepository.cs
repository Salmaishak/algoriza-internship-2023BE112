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
        public AdminRepository(VezeetaContext context) { _context = context; }
    

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
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        private string generatePassword ()

        {
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                int index = random.Next(Characters.Length);
                password.Append(Characters[index]);
            }

            return password.ToString();
        }
        public int AddDoctor(AddDoctorDTO doctor)
        {
            if (doctor != null)
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
                    password = generatePassword(), // Change the length of the password here
                    type = UserType.doctor
                };

                _context.Users.Add(doc);
                _context.SaveChanges();

                _context.Database.ExecuteSqlRaw($"insert into Doctors values ({doc.userId},{doctor.price},{doctor.specializationID});");

                _context.SaveChanges();
                return doc.userId;

            }
            else
            return -1;
        }

        public HttpStatusCode EditDoctor(int doctorID, AddDoctorDTO doctor)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode DeleteDoctor(int doctorID)
        {
            throw new NotImplementedException();
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
