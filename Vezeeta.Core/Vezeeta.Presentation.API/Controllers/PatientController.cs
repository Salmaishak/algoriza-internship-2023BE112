﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("api/patient/register/[action]")]
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
        [Route("api/patient/login/[action]")]
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
        [Route("api/patient/booking/[action]")]
        [HttpGet]
        public HttpStatusCode booking(int patientID, int SlotID, int DiscountID = 0) {

            if (SlotID != 0)
            {
                // info of Booking : doctorID, timeID, patientID 
                //timeslot : slotid, appointmentID, timeSlot 
                //appointment: day, appointmentID, doctorID 
                var query = from appointment in context.Appointments
                            join timeslot in context.TimeSlots
                            on appointment.Id equals timeslot.AppointmentID
                            select new
                            {
                                appoint = appointment,
                                time = timeslot
                            };
                var result = query.ToList();

                var targetedTimeSlot = result.Where(a => a.time.SlotId == SlotID);

                var booking = new Booking()
                {
                    DoctorID = targetedTimeSlot.Select(d => d.appoint.doctorID).FirstOrDefault(),
                    timeSlotID = targetedTimeSlot.Select(t => t.time.SlotId).FirstOrDefault(),
                    patientID = patientID,
                    BookingStatus= Status.pending
                   
                };
                context.Bookings.Add(booking);
                context.SaveChanges();

                return HttpStatusCode.OK;


            }
            else
                return HttpStatusCode.BadRequest;

            
        }

        [Route("api/patient/booking/[action]")]
        [HttpGet]
        public HttpStatusCode CancelBooking (int patientID, int BookingID)
        {
            if (BookingID != 0)
            {
                Booking booking = context.Bookings.Where<Booking>(b => b.BookingID == BookingID).FirstOrDefault();
                if (booking != null)
                {
                    booking.BookingStatus = Status.canceled;
                    context.SaveChanges();
                }
                else
                    return HttpStatusCode.Unauthorized; 
                return HttpStatusCode.OK;

            }
            else 
                return HttpStatusCode.BadRequest;
        }

        [Route("api/patient/booking/[action]")]
        [HttpGet]

        public dynamic GetAll(int userId)
        {
            var userBookings = from booking in context.Bookings
                               join doctor in context.Doctors on
                               booking.DoctorID equals doctor.doctorid
                               join users in context.Users
                               on doctor.userId equals users.userId
                               join specialization in context.Specializations
                               on doctor.specializationID equals specialization.specializationID
                               join timeslot in context.TimeSlots
                               on booking.timeSlotID equals timeslot.SlotId
                               join appointment in context.Appointments
                               on timeslot.AppointmentID equals appointment.Id
                               select new
                               {
                                   DoctorImage = doctor.image,
                                   DoctorName = doctor.fname + " " + doctor.lname,
                                   Specialization = specialization.specializationName,
                                   Day = appointment.day,
                                   Time = timeslot.Time,
                                   Price = doctor.price,
                                   DiscountCode = 0, // temp untill i change db 
                                   FinalPrice = 0, // after discount, to saved in booking
                                   Status = booking.BookingStatus
                               };

                               


            return userBookings;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
