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
       

        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo)
        {
            return repository.Add (appointmentInfo);
        }

        public dynamic GetAll(string doctorId, DayOfWeek searchDate, int pageSize = 10, int pageNumber = 1)
        {
            return repository.GetAll (doctorId, searchDate, pageSize, pageNumber);
        }

        public HttpStatusCode ConfirmCheckUp(int bookingID)
        {
           return repository.ConfirmCheckUp (bookingID);
        }

        public HttpStatusCode UpdateAppointment(int timeslotID, TimeSpan time, string doctorID)
        {
         return repository.UpdateAppointment (timeslotID, time, doctorID); 
        }

        public HttpStatusCode Delete(int timeslotID)
        {
           return repository.Delete(timeslotID);
        }
    }
}
