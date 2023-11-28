using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Repositories
{
    public interface IDoctorRepository
    {
        public bool login (string email, string password);
        public List<BookingDTO> GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1);

        public bool ConfirmCheckUp(int bookingID);

        
    }
}
