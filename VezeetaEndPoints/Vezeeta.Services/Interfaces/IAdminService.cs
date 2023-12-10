using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public dynamic getDoctorbyId(string doctorId);
        public  Task<string> AddDoctor(AddDoctorDTO doctor);

        public HttpStatusCode EditDoctor(string doctorID, AddDoctorDTO doctor);

        public  Task<HttpStatusCode> DeleteDoctor(string doctorID);
        public dynamic GetallPatients(int page, int pageSize, string search);

        public dynamic getPatientByID(string patientId);

        public HttpStatusCode AddDiscount(DiscountDTO discountInfo);

        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo);

        public HttpStatusCode DeleteDiscount(int discountID);

        public HttpStatusCode DeactivateDiscount(int discountID);
    }

}
