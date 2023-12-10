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
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public int NumOfDoctors()
        {
            return _adminService.NumOfDoctors();
        }

        [HttpGet]

        public int NumOfPatients()
        {
            return _adminService.NumOfPatients();
        }

        [HttpGet]
        public dynamic NumOfRequests()
        {
            return _adminService.NumOfRequests();
        }

        [HttpGet]
        public dynamic Top5Specializations()
        {
            return (_adminService.Top5Specializations());

        }

        [HttpGet]
        public dynamic Top10Doctors()
        {
            return _adminService.Top10Doctors();
        }



        [HttpGet]
        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
            return _adminService.GetAllDoctors(page, pageSize, search);
        }

        [HttpGet]
        public dynamic getDoctorById(string doctorID)
        {
            return _adminService.getDoctorbyId(doctorID);
        }
        [HttpPost]

        public Task<string> AddDoctor(AddDoctorDTO doctor)
        {
            return _adminService.AddDoctor(doctor);
        }
        [HttpPatch]
        public async Task<HttpStatusCode> editDoctor(string doctorID, AddDoctorDTO dto)
        {
            return _adminService.EditDoctor(doctorID, dto);

        }
        [HttpDelete]
        public async Task<HttpStatusCode> DeleteDoctor(string doctorID)
        {
            return await _adminService.DeleteDoctor(doctorID);
        }
        [HttpGet]
        public dynamic GetallPatients(string search, int page=1, int pageSize=10)
        {
            return _adminService.GetallPatients(page, pageSize, search);
        }
        [HttpGet]
        public dynamic getPatientByID(string patientId)
        {
            return _adminService.getPatientByID(patientId);
        }
        [HttpPut]
        public HttpStatusCode AddDiscount(DiscountDTO discountInfo)
        {
            return _adminService.AddDiscount(discountInfo);
        }
        [HttpPatch]
        public HttpStatusCode EditDiscount(int discountID, DiscountDTO discountInfo)
        {
            return _adminService.EditDiscount(discountID, discountInfo);
        }
        [HttpGet]
        public HttpStatusCode DeleteDiscount(int discountID)
        {
            return _adminService.DeleteDiscount(discountID);
        }
        [HttpGet]
        public HttpStatusCode DeactivateDiscount(int discountID)
        {
            return _adminService.DeactivateDiscount(discountID);
        }




    }
}
