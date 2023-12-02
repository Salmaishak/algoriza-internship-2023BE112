using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
            var booking = _context.Bookings.Where(d => d.BookingID == bookingID).FirstOrDefault();
            if (booking != null)
            {
                booking.BookingStatus = Status.completed;
                _context.SaveChanges();
                return HttpStatusCode.OK;
            }
            else return HttpStatusCode.NotFound;
        }

        public HttpStatusCode Delete(TimeSpan time, Core.Models.DayOfWeek day)
        {
            throw new NotImplementedException();
        }

        public HttpStatusCode UpdateAppointment(int timeslotID, TimeSpan time, DayOfWeek day, int doctorID)
        {
            var bookingCheck = _context.Bookings.Where(d => d.timeslot.SlotId == timeslotID &&
          d.BookingStatus == Status.pending);
            //if it exists here it is booked

            if (bookingCheck.Any())
            {
                return HttpStatusCode.Unauthorized;
            }
            else
            {   //else it doesn't exist in booking, so we it must be in appointments
                var appointment = _context.Appointments.Where(a => a.day == day).FirstOrDefault();
                var timeslot = _context.TimeSlots.Where(a => a.SlotId == timeslotID).FirstOrDefault();
                if (appointment == null || timeslot == null)
                {
                    // this day doesnt have appointments so i will display error not found, he can use Add to add in this day
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    //change time of this timeslot
                    timeslot.Time = time;
                    _context.SaveChanges();
                    return HttpStatusCode.OK;
                }


            }

        }

        public dynamic GetAll(int doctorId, DayOfWeek searchDate , int pageSize = 10, int pageNumber = 1)
        {
            var query = _context.Bookings
                .Join(
                    _context.Doctors,
                    booking => booking.DoctorID,
                    doctor => doctor.doctorid,
                    (booking, doctor) => new { Booking = booking, Doctor = doctor }
                )
                .Join(
                    _context.Users,
                    joined => joined.Booking.patientID,
                    user => user.userId,
                    (joined, user) => new { Joined = joined, User = user }
                )
                .Join(
                    _context.TimeSlots,
                    joinedUser => joinedUser.Joined.Booking.timeSlotID,
                    timeSlot => timeSlot.SlotId,
                    (joinedUser, timeSlot) => new { JoinedUser = joinedUser, TimeSlot = timeSlot }
                )
                .Join(
                    _context.Appointments,
                    joinedTimeSlot => joinedTimeSlot.TimeSlot.AppointmentID,
                    appointment => appointment.Id,
                    (joinedTimeSlot, appointment) => new { JoinedTimeSlot = joinedTimeSlot, Appointment = appointment }
                )
                .Where(joinedAppointment =>
                    joinedAppointment.JoinedTimeSlot.JoinedUser.Joined.Doctor.doctorid == doctorId &&
                  joinedAppointment.Appointment.day == searchDate
                  
                )
                .Select(joinedAppointment => new
                {
                    FullName = joinedAppointment.JoinedTimeSlot.JoinedUser.User.fname + " " +
                               joinedAppointment.JoinedTimeSlot.JoinedUser.User.lname,
                    Image = joinedAppointment.JoinedTimeSlot.JoinedUser.User.image,
                    Gender = joinedAppointment.JoinedTimeSlot.JoinedUser.User.gender,
                    PhoneNumber = joinedAppointment.JoinedTimeSlot.JoinedUser.User.phoneNumber,
                    Email = joinedAppointment.JoinedTimeSlot.JoinedUser.User.email,
                    Age = DateTime.Today.Year- joinedAppointment.JoinedTimeSlot.JoinedUser.User.dateOfBirth.Year,
                    DateTime = searchDate
                })
                .OrderByDescending(appointment => appointment.DateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return query;
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

            if (doctor != null && doctor.type== UserType.doctor)
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
