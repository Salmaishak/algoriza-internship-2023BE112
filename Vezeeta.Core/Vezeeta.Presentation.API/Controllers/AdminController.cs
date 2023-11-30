using Microsoft.AspNetCore.Mvc;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Presentation.API.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService) {
            _adminService = adminService;
        }
    
        [Route("/api/admin/[action]")]
        [HttpGet]
        public int NumOfDoctors()
        {
            return _adminService.NumOfDoctors();
        }

        [Route("/api/admin/[action]")]
        [HttpGet]

        public int NumOfPatients ()
        {
            return _adminService.NumOfPatients();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
