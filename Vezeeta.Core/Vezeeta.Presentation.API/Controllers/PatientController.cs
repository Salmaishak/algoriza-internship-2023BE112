using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Vezeeta.Core.Models;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Services.Interfaces;

namespace Vezeeta.Presentation.API.Controllers
{
    public class PatientController : Controller
    {

        private readonly IPatientService service;
        private readonly VezeetaContext context;
        public PatientController (IPatientService service, VezeetaContext context)
        {
            this.service = service;
            this.context = context;
        }
        [Route("api/patient/register")]
        [HttpGet]
        public HttpStatusCode Register(User user)
        {
            if (user != null)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return HttpStatusCode.OK;
            }
            else
                return HttpStatusCode.NotFound;
        }
        [Route("api/patient/login")]
        [HttpGet]
        public HttpStatusCode login(string email, string password) {

            if (email != String.Empty && password != String.Empty)
            {
                var patient = context.Users.Where<User>(d=> d.email == email && d.password==password).FirstOrDefault();
                if (patient != null)
                {
                    return HttpStatusCode.OK;
                }
                else return HttpStatusCode.Unauthorized;
            }
            else
             return   HttpStatusCode.Unauthorized;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
