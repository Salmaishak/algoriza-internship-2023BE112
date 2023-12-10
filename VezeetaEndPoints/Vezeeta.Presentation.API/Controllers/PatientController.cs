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
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientService service;

        public PatientController(IPatientService service)
        {
            this.service = service;
        }

        [HttpPost("Registration/[action]")]
        public Task<string> Register(PatientDTO patient)
        {
            return service.Register(patient);
        }

       

        [Authorize(Roles = "Patient")]
        [HttpPatch("Bookings/[action]")]
        public HttpStatusCode CancelBooking(string patientID, int BookingID)
        {
            return service.CancelBooking(patientID, BookingID);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("Bookings/[action]")]
        public dynamic GetAllBookings(string userId)
        {
            return service.GetAllBookings(userId);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("SearchDoctors/[action]")]
        public dynamic GetAllDoctors(int page = 1, int pageSize = 10, string search = "")
        {
            return service.GetAllDoctors(page, pageSize, search);
        }
        [Authorize(Roles = "Patient")]
        [HttpPost("SearchDoctors/[action]")]
        public HttpStatusCode Booking(string patientID, int SlotID, int DiscountID = 4)
            // DiscountID is 4, since in DB the NoDiscount ID is 4
        {
            return service.booking(patientID, SlotID, DiscountID);
        }
    }

}

