using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientRepo;

        public PatientService (IPatientRepository patientRepo)
        {
            this.patientRepo = patientRepo;
        }

        public HttpStatusCode booking(string patientID, int SlotID, int DiscountID = 0)
        {
               return  patientRepo.booking(patientID, SlotID, DiscountID);
        }

        public HttpStatusCode CancelBooking(string patientID, int BookingID)
        {
            return patientRepo.CancelBooking(patientID, BookingID);
        }

        public dynamic GetAllBookings(string userId)
        {
           return patientRepo.GetAllBookings(userId);
        }

        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
            return patientRepo.GetAllDoctors (page, pageSize, search);
        }

      
        public Task<string> Register(PatientDTO patient)
        {
         return patientRepo.Register(patient);
        }
    }
}
