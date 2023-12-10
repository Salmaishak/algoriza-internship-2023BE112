using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Services.Services
{

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository repository;
        private readonly VezeetaContext context;

        public AdminService(IAdminRepository repository, VezeetaContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
           return repository.GetAllDoctors(page, pageSize, search);
        }

        public dynamic getDoctorbyId(string doctorId)
        {
            return repository.GetDoctorById(doctorId);
                
                }

        public int NumOfDoctors()
        {
          return  repository.NumOfDoctors();
        }

        public int NumOfPatients()
        {
            return repository.NumOfPatients();
        }
        public dynamic NumOfRequests()
        { return repository.NumOfRequests(); }
        public dynamic Top10Doctors()
        {return repository.Top10Doctors();
        }

        public async Task<string> AddDoctor(AddDoctorDTO doctor)
        {   
            var ID= await repository.AddDoctor(doctor);
           var Doc= context.Users.FirstOrDefault(u => u.Id == ID.ToString());
            EmailService emailService = new EmailService();
            await emailService.SendEmail(Doc.email, Doc.password, Doc.fname);
            return ID;

        }
        public dynamic Top5Specializations()
        {
            return repository.Top5Specializations();
        }

        public HttpStatusCode EditDoctor(string doctorID, AddDoctorDTO doctor)
        {
            return repository.EditDoctor(doctorID, doctor);
            }

        public async Task<HttpStatusCode> DeleteDoctor(string doctorID)
        {
            return await repository.DeleteDoctor(doctorID);
        }

        public dynamic GetallPatients(int page, int pageSize, string search)
        {
            return repository.GetallPatients(page, pageSize, search);
        }

        public dynamic getPatientByID(string patientId)
        {
            return repository.getPatientByID(patientId);
        }

        public HttpStatusCode AddDiscount(DiscountDTO discountInfo)
        {
           return repository.AddDiscount(discountInfo);
        }

        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo)
        {
            return repository.EditDiscount(discountID, discountInfo);
        }

        public HttpStatusCode DeleteDiscount(int discountID)
        {
            return repository.DeleteDiscount(discountID);
        }

        public HttpStatusCode DeactivateDiscount(int discountID)
        {
           return repository.DeactivateDiscount(discountID);
        }
    }
}
