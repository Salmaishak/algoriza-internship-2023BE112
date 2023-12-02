using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Presentation.API.Controllers
{

    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly VezeetaContext _context;
        public AdminController(IAdminService adminService, VezeetaContext context) {
            _adminService = adminService;
            _context = context;
        }
    
        [Route("/api/admin/dashboard/[action]")]
        [HttpGet]
        public int NumOfDoctors()
        {
            return _adminService.NumOfDoctors();
        }

        [Route("/api/admin/dashboard/[action]")]
        [HttpGet]

        public int NumOfPatients ()
        {
            return _adminService.NumOfPatients();
        }

        [Route ("/api/admin/dashboard/[action]")]
        [HttpGet]
        public IActionResult NumOfRequests ()
        {
            int totalRequests = _context.Bookings.Count();
            int pendingRequests = _context.Bookings.Count(d => d.BookingStatus == Core.Models.Status.pending);
            int canceledRequests = _context.Bookings.Count(d=>d.BookingStatus == Core.Models.Status.canceled);
            int completedRequests = _context.Bookings.Count(d => d.BookingStatus == Core.Models.Status.completed);
            return Json(new { totalRequests , pendingRequests, canceledRequests, completedRequests });
            
            
        }

        [Route("/api/admin/dashboard/[action]")]
        [HttpGet]
        public IActionResult Top5Specializations()
         {
            var result = _context.Bookings
            .Join(
                _context.Doctors,
                booking => booking.DoctorID,
                doctor => doctor.doctorid,
                (booking, doctor) => new { Booking = booking, Doctor = doctor }
            )
            .Join(
                _context.Specializations,
                joined => joined.Doctor.specializationID,
                specialization => specialization.specializationID,
                (joined, specialization) => new { Joined = joined, Specialization = specialization }
            )
            .GroupBy(
                joinedSpecialization => joinedSpecialization.Specialization.specializationName,
                joinedSpecialization => joinedSpecialization.Joined.Booking.BookingID,
                (specializationName, count) => new
                {
                    SpecializationName = specializationName,
                    Count = count.Count()
                }
            ).OrderByDescending(result => result.Count) // Order by request count in descending order
            .Take(5); ;

            return Ok( result );

        }

        [Route("/api/admin/dashboard/[action]")]
        [HttpGet]
        public IActionResult Top10Doctors ()
        {
                var result = _context.Bookings
        .Join(
            _context.Doctors,
            booking => booking.DoctorID,
            doctor => doctor.doctorid,
            (booking, doctor) => new { Booking = booking, Doctor = doctor }
        )
        .Join(
            _context.Users,
            joined => joined.Doctor.userId,
            user => user.userId,
            (joined, user) => new { Joined = joined, User = user }
        )
        .GroupBy(
            joinedUser => joinedUser.User.fname + " " + joinedUser.User.lname,
            joinedUser => joinedUser.Joined.Booking.BookingID,
            (fullname, count) => new
            {
                FullName = fullname,
                Requests = count.Count()
            }
        ).OrderByDescending(result => result.Requests) 
    .Take(10); ;

            return Ok(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
