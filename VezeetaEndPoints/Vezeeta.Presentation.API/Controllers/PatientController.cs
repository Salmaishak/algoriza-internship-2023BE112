using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PatientController : Controller
    {

        private readonly IPatientService service;

        public PatientController(IPatientService service)
        {
            this.service = service;
        }

        [HttpPost]
        public Task<string> Register(PatientDTO patient)
        {
          return service.Register(patient);
        }
       
        [Authorize(Roles = "Patient")]
        [HttpPost]
        public HttpStatusCode booking(int patientID, int SlotID, int DiscountID = 0) {
            
            return service.booking(patientID, SlotID, DiscountID);
          
        }
        [Authorize(Roles = "Patient")]
        [HttpPatch]
        public HttpStatusCode CancelBooking(int patientID, int BookingID)
        {
            return service.CancelBooking(patientID, BookingID);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet]

        public dynamic GetAll(int userId)
        {
            return service.GetAllBookings(userId);
        }
        [Authorize(Roles = "Patient")]
        [HttpGet]

        public dynamic GetAllDoctors(int page=1, int pageSize=10, string search="") {

           return service.GetAllDoctors(page, pageSize, search);
        }
    
    }
}
