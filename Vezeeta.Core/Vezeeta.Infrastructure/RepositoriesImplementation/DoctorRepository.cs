using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Infrastructure.RepositoriesImplementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VezeetaContext _context;

        public DoctorRepository(VezeetaContext context) { _context = context; }
        /// <summary>
        /// Add Appointments
        /// </summary>
        /// <param name="price"></param>
        /// <param name="days"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public HttpStatusCode Add(int doctorId, float price, List<(DayOfWeek Day, List<TimeSpan> Times)> days)
  {
      try
      {
          var doctor = _context.Doctors.FirstOrDefault<Doctor>(d => d.doctorid == doctorId);

          if (doctor != null)
          {
              doctor.price = price;

             

              _context.SaveChanges();
              return HttpStatusCode.OK; // Success
          }

          return HttpStatusCode.Unauthorized;
      }
      catch (Exception ex)
      {
          // Log or handle the exception
          Console.WriteLine(ex.Message);
          return HttpStatusCode.InternalServerError; // Or handle specific errors accordingly
      }
  }


        public HttpStatusCode ConfirmCheckUp(int bookingID)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Edit(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get All bookings 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="searchDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public dynamic GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1)
        {
            return null;
        }


        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Check if the birthday has occurred this year
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }


        public HttpStatusCode login(string email, string password)
        {
            var doctor = _context.Users.FirstOrDefault<User>(e =>e.email.Equals(email));

            if (doctor != null && doctor.type == UserType.doctor)
            {
                if (doctor.password == password)
                return HttpStatusCode.OK; 
                else 
                    return HttpStatusCode.Unauthorized; 
            }
            return HttpStatusCode.Unauthorized;
        }
    }
}
