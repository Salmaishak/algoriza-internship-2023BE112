using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;

namespace Vezeeta.Core.Repositories
{
    internal class DoctorRepository : IDoctorRepository
    {
        private readonly VezeetaContext _context;
        bool IDoctorRepository.Add(float price, List<Models.DayOfWeek> days, List<TimeSpan> time)
        {
            throw new NotImplementedException();
        }

        bool IDoctorRepository.ConfirmCheckUp(int bookingID)
        {
            throw new NotImplementedException();
        }

        bool IDoctorRepository.Delete(TimeSpan time, Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        bool IDoctorRepository.Edit(TimeSpan time, Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        dynamic IDoctorRepository.GetAll(int doctorId, DateTime? searchDate, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public bool Login(string email, string password)
        {

            var doctor = _context.Find<User>(email);

            // the email should be unique 
            if (doctor != null && doctor.type == UserType.doctor)
            {
                if (doctor.password == password)
                {
                    return true; // Login successful
                }
            }

            return false;
        }
    }
}
