using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Core.Repositories
{
    public interface IDoctorRepository
    {
        public HttpStatusCode login (string email, string password);
        public dynamic GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1);

        public HttpStatusCode ConfirmCheckUp(int bookingID);

        public HttpStatusCode Add(int doctorId, float price, List<(DayOfWeek Day, List<TimeSpan> Times)> days);

        public HttpStatusCode Edit(TimeSpan time, DayOfWeek day);

        public HttpStatusCode Delete(TimeSpan time, DayOfWeek day);
    }
}
