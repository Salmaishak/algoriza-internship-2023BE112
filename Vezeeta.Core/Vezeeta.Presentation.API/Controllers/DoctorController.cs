using Microsoft.AspNetCore.Mvc;
using Vezeeta.Services.Interfaces;
using Vezeeta.Services.Services;

namespace Vezeeta.Presentation.API.Controllers
{
   
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        [Route("/api/doctors/[action]")]
        [HttpGet]
        public bool login (string email, string password)
        {
            return doctorService.login (email, password);

        }

            public IActionResult Index()
        {
            return View();
        }
    }
}
