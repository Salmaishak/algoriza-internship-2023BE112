using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;

namespace Vezeeta.Services.Interfaces
{
    public interface IAdminService
    {
        public int NumOfDoctors();
        public int NumOfPatients();
        public dynamic NumOfRequests();
        public dynamic Top5Specializations();
        public dynamic Top10Doctors();

        public dynamic GetAllDoctors(int page, int pageSize, string search);
        public dynamic getDoctorbyId(int doctorId);
        public int addDoctor(AddDoctorDTO dto);
    }
}
