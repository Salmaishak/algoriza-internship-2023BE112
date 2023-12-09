using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
        [Route("api/patient/register/[action]")]
        [HttpGet]
        public HttpStatusCode Register(User user)
        {
          return service.Register(user);
        }
        [Route("api/patient/login/[action]")]
        [HttpGet]
        public HttpStatusCode login(string email, string password) {

          return  service.PatientLogin(email, password);
           
        }
        [Route("api/patient/searchDoctors/booking/[action]")]
        [HttpPost]
        public HttpStatusCode booking(int patientID, int SlotID, int DiscountID = 0) {
            
            return service.booking(patientID, SlotID, DiscountID);
          
        }

        [Route("api/patient/booking/[action]")]
        [HttpPatch]
        public HttpStatusCode CancelBooking(int patientID, int BookingID)
        {
            return service.CancelBooking(patientID, BookingID);
        }

        [Route("api/patient/booking/[action]")]
        [HttpGet]

        public dynamic GetAll(int userId)
        {
            return service.GetAllBookings(userId);
        }

        [Route("api/patient/searchDoctors/Getall")]
        [HttpGet]

        public dynamic GetAllDoctors(int page=1, int pageSize=10, string search="") {

           return service.GetAllDoctors(page, pageSize, search);
        }
    
    }
}
