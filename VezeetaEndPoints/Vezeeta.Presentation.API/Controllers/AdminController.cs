using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Services.Interfaces;
using Vezeeta.Services.Services;

namespace Vezeeta.Presentation.API.Controllers
{

    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly VezeetaContext _context;
        private readonly IEmailService emailservice;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(IAdminService adminService, VezeetaContext context,
            IEmailService email, UserManager<IdentityUser> userManager)
        {
            _adminService = adminService;
            _context = context;
            emailservice = email;
            _userManager = userManager;
        }

        [HttpGet("Dashboard/[action]")]
        public int NumOfDoctors()
        {
            return _adminService.NumOfDoctors();
        }

        [HttpGet("Dashboard/[action]")]

        public int NumOfPatients()
        {
            return _adminService.NumOfPatients();
        }

        [HttpGet("Dashboard/[action]")]
        public dynamic NumOfRequests()
        {
            return _adminService.NumOfRequests();
        }

        [HttpGet("Dashboard/[action]")]
        public dynamic Top5Specializations()
        {
            return (_adminService.Top5Specializations());

        }

        [HttpGet("Dashboard/[action]")]
        public dynamic Top10Doctors()
        {
            return _adminService.Top10Doctors();
        }



        [HttpGet("Doctors/[action]")]
        public dynamic GetAllDoctors(string search, int page=1, int pageSize=10 )
        {
            return _adminService.GetAllDoctors(page, pageSize, search);
        }

        [HttpGet("Doctors/[action]")]
        public dynamic getDoctorById(string doctorID)
        {
            return _adminService.getDoctorbyId(doctorID);
        }
        [HttpPost("Doctors/[action]")]

        public Task<string> AddDoctor(AddDoctorDTO doctor)
        {
            return _adminService.AddDoctor(doctor);
        }
        [HttpPatch("Doctors/[action]")]
        public async Task<HttpStatusCode> editDoctor(string doctorID, AddDoctorDTO dto)
        {
            return _adminService.EditDoctor(doctorID, dto);

        }
        [HttpDelete("Doctors/[action]")]
        public async Task<HttpStatusCode> DeleteDoctor(string doctorID)
        {
            return await _adminService.DeleteDoctor(doctorID);
        }
        [HttpGet("Patients/[action]")]
        public dynamic GetallPatients(string search, int page=1, int pageSize=10)
        {
            return _adminService.GetallPatients(page, pageSize, search);
        }
        [HttpGet("Patients/[action]")]
        public dynamic getPatientByID(string patientId)
        {
            return _adminService.getPatientByID(patientId);
        }
        [HttpPut("Setting/[action]")]
        public HttpStatusCode AddDiscount(DiscountDTO discountInfo)
        {
            return _adminService.AddDiscount(discountInfo);
        }
        [HttpPatch("Setting/[action]")]
        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo)
        {
            return _adminService.EditDiscount(discountID, discountInfo);
        }
        [HttpGet("Setting/[action]")]
        public HttpStatusCode DeleteDiscount(int discountID)
        {
            return _adminService.DeleteDiscount(discountID);
        }
        [HttpGet("Setting/[action]")]
        public HttpStatusCode DeactivateDiscount(int discountID)
        {
            return _adminService.DeactivateDiscount(discountID);
        }




    }
}
