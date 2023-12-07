using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.Repositories
{
    public interface IAdminRepository
    {
        public int NumOfDoctors();
        public int NumOfPatients();
        public dynamic NumOfRequests();
        public dynamic Top5Specializations();
        public dynamic Top10Doctors();

        public dynamic GetAllDoctors(int page, int pageSize, string search);
        public dynamic GetDoctorById (int id);

        public int AddDoctor(AddDoctorDTO doctor);

        public HttpStatusCode EditDoctor(int doctorID, AddDoctorDTO doctor);

        public HttpStatusCode DeleteDoctor(int doctorID);

        public dynamic GetallPatients(int page, int pageSize, string search);

        public dynamic getPatientByID(int patientId);

        public HttpStatusCode AddDiscount(DiscountDTO discountInfo);

        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo);

        public HttpStatusCode DeleteDiscount(int discountID);

        public HttpStatusCode DeactivateDiscount (int discountID);

    }
}
