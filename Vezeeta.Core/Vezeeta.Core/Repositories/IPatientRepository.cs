using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Core.Repositories
{
    public interface IPatientRepository
    {
        public bool Register(User user);
        public bool PatientLogin (string email,  string password);
        public dynamic GetAllDoctors (int page, int pageSize, string search);
        public bool Booking(TimeSpan time, DayOfWeek day, Discount discount = null);

        public dynamic GetAllBookings();
        public bool CancelBooking(TimeSpan time, DayOfWeek day);
        
    }
}
