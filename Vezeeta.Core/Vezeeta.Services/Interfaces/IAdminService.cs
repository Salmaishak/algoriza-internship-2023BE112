using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Services.Interfaces
{
    public interface IAdminService
    {
        public int NumOfDoctors();
        public int NumOfPatients();
   
        public dynamic Top5Specializations();
        public dynamic Top10Doctors();
    }
}
