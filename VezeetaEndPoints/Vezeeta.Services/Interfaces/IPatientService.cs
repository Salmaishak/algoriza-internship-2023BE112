using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Services.Interfaces
{
    public interface IPatientService
    {
        public Task<string> Register(PatientDTO patient);
        public dynamic GetAllDoctors(int page, int pageSize, string search);
        public HttpStatusCode booking(string patientID, int SlotID, int DiscountID = 0);

         public dynamic GetAllBookings(string userId);
        public HttpStatusCode CancelBooking(string patientID, int BookingID);


    }
}
