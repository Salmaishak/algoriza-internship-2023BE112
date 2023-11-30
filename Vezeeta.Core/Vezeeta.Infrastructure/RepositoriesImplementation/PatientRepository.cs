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
    public class PatientRepository : IPatientRepository
    {
        private readonly VezeetaContext _context;
        public PatientRepository (VezeetaContext context)
        {
            _context =context ;
        }

        public bool Booking(TimeSpan time, Core.Models.DayOfWeek day, Discount discount = null)
        {
            throw new NotImplementedException();
        }

        public bool CancelBooking(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        public dynamic GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public bool PatientLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
