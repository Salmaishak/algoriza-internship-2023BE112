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
        public string GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1);

        public HttpStatusCode ConfirmCheckUp(int bookingID);

        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo);

        public HttpStatusCode Edit(TimeSpan time, DayOfWeek day);

        public HttpStatusCode Delete(TimeSpan time, DayOfWeek day);
    }
}
