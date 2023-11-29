using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
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

        [Route("/api/doctors/[action]")]
        [HttpPost]
        public HttpStatusCode Add( IDictionary<DayOfWeek, List<TimeSpan>> times)
        {
            int doctorId = 1;
            float price = 44;
            var doctor = _context.Doctors.FirstOrDefault<Doctor>(d => d.doctorid == doctorId);
            int lastAppointmentID = _context.Appointments.OrderByDescending(a => a.Id).FirstOrDefault()?.Id ?? 0;

            List<Appointment> appointments = new List<Appointment>();
            
            if (doctor != null && times != null)
            {
                doctor.price = price;

                foreach (var entry in times)
                {
                    DayOfWeek day = entry.Key;
                    List<TimeSpan> timeSlots = entry.Value;
                    _context.Appointments.Add(new Appointment() { day= day, doctorID= doctor.doctorid});
                    lastAppointmentID++;
                    _logger.LogInformation($"Day: {day}");
                    _logger.LogInformation($"TimeSlots: {timeSlots.Count}");
                    foreach (var item in timeSlots)
                    {
                        _logger.LogInformation($"TimeSlot: {item}");
                        _context.TimeSlots.Add(new TimeSlot() { AppointmentID = lastAppointmentID, Time = item });

                    }
                }
              
               
                _context.SaveChanges();
                return HttpStatusCode.Accepted;

            }

            return HttpStatusCode.Unauthorized; // Doctor not found 401
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
