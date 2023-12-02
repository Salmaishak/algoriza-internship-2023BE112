using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Presentation.API.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Services.Interfaces
{
    public interface IDoctorService
    {
        public HttpStatusCode login(string email, string password);
        public dynamic GetAll(int doctorId, DayOfWeek searchDate, int pageSize = 10, int pageNumber = 1);

        public HttpStatusCode ConfirmCheckUp(int bookingID);

        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo);

        public HttpStatusCode UpdateAppointment(int timeslotID, TimeSpan time, DayOfWeek day, int doctorID);

        public HttpStatusCode Delete(TimeSpan time, DayOfWeek day);
    }
}
