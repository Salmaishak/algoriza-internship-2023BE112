using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Core.Repositories
{
    public interface IDoctorRepository
    {
        public bool login (string email, string password);
        public List<DoctorDTO> GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1);

        public bool ConfirmCheckUp(int bookingID);

        public bool Add(float price, List<DayOfWeek> days, List<TimeSpan> time);

        public bool Edit(TimeSpan time, DayOfWeek day);

        public bool Delete(TimeSpan time, DayOfWeek day);
    }
}
