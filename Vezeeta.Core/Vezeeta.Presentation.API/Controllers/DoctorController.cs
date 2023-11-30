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

        [Route("/api/doctors/[action]")]
        [HttpPost]
      
        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo)
        {
            // get the doctor who is adding the information 
            var doctor = _context.Doctors.FirstOrDefault<Doctor>(d => d.doctorid == appointmentInfo.doctorId);
            // get last appointment ID to add on it
            int lastAppointmentID = _context.Appointments.OrderByDescending(a => a.Id).FirstOrDefault()?.Id ?? 0;
            // if the doctor is null (Unauthorized) 
            if (doctor != null )
            {
                //change the price
                doctor.price = appointmentInfo.price;
              
                foreach (var entry in appointmentInfo.times)
                {
                    DayOfWeek dayWeek = entry.Key;
                    List<TimeSpan> timeSlots = entry.Value;
                    _context.Appointments.Add(new Appointment() { day= dayWeek, doctorID= doctor.doctorid});
                    lastAppointmentID++;
                    _logger.LogInformation($"Day: {dayWeek}");
                    _logger.LogInformation($"TimeSlots: {timeSlots.Count}");
                    _context.SaveChanges();
                   
                    foreach (var item in timeSlots)
                    {
                        _logger.LogInformation($"TimeSlot: {item}");
                        _context.TimeSlots.Add(new TimeSlot() { AppointmentID = lastAppointmentID, Time = item });
                        _context.SaveChanges();
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
