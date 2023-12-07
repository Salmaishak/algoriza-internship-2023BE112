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

        public dynamic getDoctorbyId(int doctorId)
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

        public  int addDoctor (AddDoctorDTO dto)
        {
           return repository.AddDoctor(dto);
            

        }
        public dynamic Top5Specializations()
        {
            return repository.Top5Specializations();
        }

        public HttpStatusCode EditDoctor(int doctorID, AddDoctorDTO doctor)
        {
            return repository.EditDoctor(doctorID, doctor);
            }

        public HttpStatusCode DeleteDoctor(int doctorID)
        {
            throw new NotImplementedException();
        }

        public dynamic GetallPatients(int page, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public dynamic getPatientByID(int patientId)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode AddDiscount(DiscountDTO discountInfo)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode DeleteDiscount(int discountID)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode DeactivateDiscount(int discountID)
        {
            throw new NotImplementedException();
        }
    }
}
