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

        public int NumOfPatients ()
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
        public dynamic Top10Doctors ()
        {
             return _adminService.Top10Doctors();
        }

        

        [HttpGet]
        public dynamic GetAllDoctors(int page, int pageSize, string search)
        {
            return _adminService.GetAllDoctors(page, pageSize, search); 
        }

        [HttpGet]
        public dynamic getDoctorById( int doctorID)
        {
            return _adminService.getDoctorbyId (doctorID);
        }
        [HttpPost]

        public async Task<IdentityResult> AddDoctor(AddDoctorDTO doctor)
        {
            if (doctor != null)
            {
                User doc = new User()
                {
                    UserName = doctor.email,
                    Email = doctor.email,
                                      fname = doctor.fname,
                    lname = doctor.lname,
                    email = doctor.email,
                    dateOfBirth = doctor.dateOfBirth,
                    image = doctor.image,
                    phoneNumber = doctor.phone,
                    gender = doctor.gender,
                    password = "Abc@235!", // Change the length of the password here
                    type = UserType.admin
                };
                var result = await _userManager.CreateAsync(doc, doc.password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(doc, "Doctor");
                    _context.Users.Add(doc);
                    //_context.SaveChanges();

                    //_context.Database.ExecuteSqlRaw($"insert into Doctors values (\"doctor\"+{doc.userId}+\"_id\",{doc.userId},{doctor.price},{doctor.specializationID});");

                    //_context.SaveChanges();
                    return result;
                }
                else
                    return result;


            }
            else
                return null;

        }
        [HttpPatch]
        public async Task<HttpStatusCode> editDoctor(int doctorID, AddDoctorDTO dto)
        {
            return _adminService.EditDoctor(doctorID, dto);

        }


        

    }
}
