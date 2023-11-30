using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;
using Vezeeta.Presentation.API.Models;
using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Infrastructure.RepositoriesImplementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VezeetaContext _context;

        public DoctorRepository(VezeetaContext context) { _context = context; }
     

        public HttpStatusCode ConfirmCheckUp(int bookingID)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Delete(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode Edit(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get All bookings 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="searchDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public dynamic GetAll(int doctorId, DateTime? searchDate = null, int pageSize = 10, int pageNumber = 1)
        {
            return null;
        }


        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Check if the birthday has occurred this year
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
        public HttpStatusCode Add(AddAppointmentDTO appointmentInfo)
        {
            // get the doctor who is adding the information 
            var doctor = _context.Doctors.FirstOrDefault<Doctor>(d => d.doctorid == appointmentInfo.doctorId);
            // get last appointment ID to add on it
            int lastAppointmentID = _context.Appointments.OrderByDescending(a => a.Id).FirstOrDefault()?.Id ?? 0;
            // if the doctor is null (Unauthorized) 
            if (doctor != null)
            {
                //change the price
                doctor.price = appointmentInfo.price;

                foreach (var entry in appointmentInfo.times)
                {
                    DayOfWeek dayWeek = entry.Key;
                    List<TimeSpan> timeSlots = entry.Value;
                    _context.Appointments.Add(new Appointment() { day = dayWeek, doctorID = doctor.doctorid });
                    lastAppointmentID++;
                   
                    _context.SaveChanges();

                    foreach (var item in timeSlots)
                    {
                        
                        _context.TimeSlots.Add(new TimeSlot() { AppointmentID = lastAppointmentID, Time = item });
                        _context.SaveChanges();
                    }
                }


                _context.SaveChanges();
                return HttpStatusCode.Accepted;

            }

            return HttpStatusCode.Unauthorized; // Doctor not found 401
        }

        public HttpStatusCode login(string email, string password)
        {
            var doctor = _context.Users.FirstOrDefault<User>(e =>e.email.Equals(email));

            if (doctor != null && doctor.type == UserType.doctor)
            {
                if (doctor.password == password)
                return HttpStatusCode.OK; 
                else 
                    return HttpStatusCode.Unauthorized; 
            }
            return HttpStatusCode.Unauthorized;
        }
    }
}
