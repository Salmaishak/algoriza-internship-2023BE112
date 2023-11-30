using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Infrastructure.RepositoriesImplementation;
using Vezeeta.Presentation.API.Models;
using Vezeeta.Services.Interfaces;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository repository;

        public DoctorService(IDoctorRepository repository)
        {
            this.repository = repository;
        }
        public HttpStatusCode login (string email, string password)
        {


            HttpStatusCode tr = repository.login (email, password);
                return tr;
            
          
           
        }

        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo)
        {
            return repository.Add (appointmentInfo);
        }
    }
}
