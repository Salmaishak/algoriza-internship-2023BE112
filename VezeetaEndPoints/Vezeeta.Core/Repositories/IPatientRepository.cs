using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Core.Repositories
{
    public interface IPatientRepository
    {
        public HttpStatusCode Register(User user);
        public HttpStatusCode PatientLogin (string email,  string password);
        public dynamic GetAllDoctors (int page, int pageSize, string search);
        public HttpStatusCode booking(int patientID, int SlotID, int DiscountID = 0);

         public dynamic GetAllBookings(int userId);
        public HttpStatusCode CancelBooking(int patientID, int BookingID);


    }
}
