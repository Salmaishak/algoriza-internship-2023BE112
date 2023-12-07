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
        private readonly IEmailService emailservice;
        public AdminController(IAdminService adminService, VezeetaContext context, IEmailService email) {
            _adminService = adminService;
            _context = context;
            emailservice = email;
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
           var userDoc = _context.Users.FirstOrDefault(u => u.userId == AddeddoctorID);
            return  await emailservice.SendEmail(userDoc.email, userDoc.password, (userDoc.fname +" "+ userDoc.lname));

        }
        [Route("/api/admin/doctors/[action]")]
        [HttpPatch]
        public async Task<HttpStatusCode> editDoctor(int doctorID, AddDoctorDTO dto)
        {
            return _adminService.EditDoctor(doctorID, dto);

        }


        public IActionResult Index()
        {
            return View();
        }



    }
}
