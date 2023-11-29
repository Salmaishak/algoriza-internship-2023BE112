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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VezeetaContext _context;

        public DoctorRepository(VezeetaContext context) { _context = context; }
        public bool Add(float price, List<Core.Models.DayOfWeek> days, List<TimeSpan> time)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmCheckUp(int bookingID)
        {
            throw new NotImplementedException();
        }

        public bool Delete(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        public bool Edit(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        public dynamic GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1)
        {
            throw new NotImplementedException();
        }

        public bool login(string email, string password)
        {
            var doctor = _context.Users.FirstOrDefault<User>(e =>e.email.Equals(email));

            if (doctor != null && doctor.type == UserType.doctor)
            {
                if (doctor.password == password)
                return true; 
                else 
                    return false; 
            }
            return false;
        }
    }
}
