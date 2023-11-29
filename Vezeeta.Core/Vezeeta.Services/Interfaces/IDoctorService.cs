using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Services.Interfaces
{
    public interface IDoctorService
    {
        public HttpStatusCode login(string email, string password);
        public HttpStatusCode Add(int doctorId, float price, List<(DayOfWeek Day, List<TimeSpan> Times)> days);
    }
}
