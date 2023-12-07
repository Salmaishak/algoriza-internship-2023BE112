using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Presentation.API.Models;
using Vezeeta.Services.Interfaces;
using Vezeeta.Services.Services;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Presentation.API.Controllers
{
   
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly VezeetaContext _context;
        private readonly ILogger<DoctorController> _logger;
        public DoctorController(IDoctorService doctorService, VezeetaContext context, ILogger<DoctorController> logger)
        {
            this.doctorService = doctorService;
            this._context = context;
            this._logger = logger;
        }

        [Route("/api/doctors/[action]")]
        [HttpGet]
        public HttpStatusCode login (string email, string password)
        {
            return doctorService.login (email, password);

        }

        [Route("/api/doctors/setting/[action]")]
        [HttpPost]
      
        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo)
        {
           return doctorService.Add(appointmentInfo);
        }

        [Route("/api/doctors/[action]")]
        [HttpPatch]
        public HttpStatusCode confirmcheckup (int bookingid)
        {
           return doctorService.ConfirmCheckUp(bookingid);
         
        }
        [Route("/api/doctors/booking/[action]")]
        [HttpGet]
        public dynamic GetAll(int doctorId, DayOfWeek searchDate, int pageSize = 10, int pageNumber = 1)
        {
            return doctorService.GetAll(doctorId, searchDate, pageSize, pageNumber);
        }
        [Route("/api/doctors/setting/Update")]
        [HttpPatch]
        public HttpStatusCode UpdateAppointment (int timeslotID,TimeSpan time, DayOfWeek day, int doctorID)
        {
            return doctorService.UpdateAppointment(timeslotID, time, day, doctorID);

        }

        [Route("/api/doctors/setting/[action]")]
        [HttpDelete]
        public HttpStatusCode Delete (int timeslotID)
        {
            return doctorService.Delete(timeslotID);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
