using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Services.Interfaces;
using Vezeeta.Services.Services;

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
        public dynamic NumOfRequests()
        {
            return _adminService.NumOfRequests();
        }

        [Route("/api/admin/dashboard/[action]")]
        [HttpGet]
        public dynamic Top5Specializations()
        {
          return (_adminService.Top5Specializations());

        }

        [Route("/api/admin/dashboard/[action]")]
        [HttpGet]
        public dynamic Top10Doctors ()
        {
             return _adminService.Top10Doctors();
        }

        [Route("/api/admin/doctors/[action]")]
        [HttpGet]
        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
            return _adminService.GetAllDoctors(page, pageSize, search); 
        }

        [Route("/api/admin/doctors/[action]")]
        [HttpGet]
        public dynamic getDoctorById( int doctorID)
        {
            return _adminService.getDoctorbyId (doctorID);
        }
        [Route("/api/admin/doctors/[action]")]
        [HttpPost]

        public async Task<HttpStatusCode> addDoctor (AddDoctorDTO dto)
        {
            int AddeddoctorID = _adminService.addDoctor(dto);
            EmailService emailService = new EmailService();
            var userDoc = _context.Users.FirstOrDefault(u => u.userId == AddeddoctorID);
            return  await emailService.SendEmail(userDoc.email, userDoc.password, (userDoc.fname +" "+ userDoc.lname));

        }


        public IActionResult Index()
        {
            return View();
        }



    }
}
