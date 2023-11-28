using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Repositories
{
    public interface IAdminRepository
    {
        public int NumOfDoctors();
        public int NumOfPatients();
        public int NumOfAppointments();
        public dynamic Top5Specializations();
        public dynamic Top10Doctors();

    }
}
