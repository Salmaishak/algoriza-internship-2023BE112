using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
    }
}
