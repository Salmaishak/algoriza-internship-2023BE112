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

        public IActionResult Index()
        {
            return View();
        }
    }
}
