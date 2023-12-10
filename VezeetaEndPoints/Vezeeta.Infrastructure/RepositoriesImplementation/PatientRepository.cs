using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.DTOs;
using Vezeeta.Core.Models;
using Vezeeta.Core.Repositories;
using Vezeeta.Infrastructure.DbContexts;

namespace Vezeeta.Infrastructure.RepositoriesImplementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly VezeetaContext context;
        private readonly UserManager<IdentityUser> _userManager;

        public PatientRepository (VezeetaContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

       
        // booking function for patient
        public HttpStatusCode booking(string patientID, int SlotID, int DiscountID = 4)
        {
            int finalPrice = 0;
            if (SlotID != 0)
            {   
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
                var doctorID = targetedTimeSlot.Select(d => d.appoint.doctorID).FirstOrDefault();

                if (!IsTimeSlotBooked(doctorID, SlotID)) // Check if the timeslot is already booked
                {
                    var countOfRequests = context.Bookings.Where(b => b.patientID == patientID).Count();
                    var doctorPrice = context.Doctors.FirstOrDefault(d => d.Id == doctorID).price;
                    if (IsDiscountEligible(DiscountID, patientID, countOfRequests, doctorID))
                    { 
                        // if discount is eligible calculate the discount value
                        var discount = context.Discounts.FirstOrDefault(d => d.discountID == DiscountID);


                        finalPrice = CalculateFinalPrice((int)doctorPrice, discount.valueOfDiscount, discount.discountType);
                    }
                    else
                        finalPrice = (int)doctorPrice;
                    //other wise the finalprice is the doctor's original price

                    var booking = new Booking()
                    {
                        DoctorID = doctorID,
                        timeSlotID = targetedTimeSlot.Select(t => t.time.SlotId).FirstOrDefault(),
                        patientID = patientID,
                        BookingStatus = Status.pending,
                        DiscountId = DiscountID,
                        finalPrice = finalPrice
                    };
                    // add the booking to database
                    context.Bookings.Add(booking);
                    context.SaveChanges();

                    return HttpStatusCode.OK;
                }
                else
                {
                    // Timeslot is already booked, return conflict status
                    return HttpStatusCode.Conflict;
                }
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode CancelBooking(string patientID, int BookingID)
        {

            if (BookingID != 0)
            {
                Booking booking = context.Bookings.Where<Booking>(b => b.BookingID == BookingID).FirstOrDefault();
                 
                // check the booking ID exists
                if (booking != null&& booking.patientID==patientID)
                {
                    booking.BookingStatus = Status.canceled; // Change Status to Canceled
                    context.SaveChanges();
                    return HttpStatusCode.OK;
                }
                else
                    return HttpStatusCode.Unauthorized;
               

            }
            else
                return HttpStatusCode.BadRequest;
        }

        public dynamic GetAllBookings(string userId) 
        {
            var userBookings = from booking in context.Bookings
                               join doctor in context.Doctors on
                               booking.DoctorID equals doctor.Id
                               join users in context.Users
                               on doctor.Id equals users.Id
                               join specialization in context.Specializations
                               on doctor.specializationID equals specialization.specializationID
                               join timeslot in context.TimeSlots
                               on booking.timeSlotID equals timeslot.SlotId
                               join appointment in context.Appointments
                               on timeslot.AppointmentID equals appointment.Id
                               where booking.patientID ==userId
                               select new
                               {
                                   DoctorImage = doctor.image,
                                   DoctorName = doctor.fname + " " + doctor.lname,
                                   Specialization = specialization.specializationName,
                                   Day = appointment.day,
                                   Time = timeslot.Time,
                                   Price = doctor.price,
                                   DiscountCode = booking.DiscountId, 
                                   FinalPrice = booking.finalPrice,
                                   Status = booking.BookingStatus
                               }; 
            // to get a specific output , we join tables together

            return userBookings;
        }

            public dynamic GetAllDoctors(int page, int pageSize, string search)
            {
                var doctors = context.Doctors
         .Select(doctor => new
         {
             doctor.Id,
             doctor.price,

             specialization = context.Specializations
                         .Where(s => s.specializationID == doctor.specializationID)
                         .Select(s => s.specializationName)
                         .FirstOrDefault(),
             doctorAppointments = context.Appointments
                         .Where(a => a.doctorID == doctor.Id)
                         .Join(
                             context.TimeSlots,
                             appointment => appointment.Id,
                             timeSlot => timeSlot.AppointmentID,
                             (appointment, timeSlot) => new
                             {
                                 day = appointment.day.ToString(),
                                 timeSlot.Time
                             })
                         .ToList(),
             doctor.fname,
             doctor.lname,
             doctor.email,
             doctor.image,
             doctor.phoneNumber,
             gender = doctor.gender.ToString()
         })
                 .ToList();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchedDocs = from d in doctors
                                       where d.fname.Contains(search) ||
                                       d.email.Contains(search) ||
                                       d.specialization.Contains(search) ||
                                       d.gender.Contains(search) ||
                                       d.phoneNumber.Contains(search) ||
                                       d.doctorAppointments.Select(a => a.day).Contains(search)
                                       select d;

                    var paginationDoc = searchedDocs.Skip((page - 1) * pageSize).Take(pageSize);
                    if (searchedDocs != null)
                    {
                        return paginationDoc;
                    }
                    else
                        return doctors.Skip((page - 1) * pageSize).Take(pageSize);


                }
                else
                    return doctors.Skip((page - 1) * pageSize).Take(pageSize);


            }


        public async Task<string> Register(PatientDTO patient)
        {
            if (patient != null)
            {
                User user = new User()
                {
                    UserName = patient.email,
                    Email = patient.email,
                    fname = patient.fname,
                    lname = patient.lname,
                    email = patient.email,
                    dateOfBirth = patient.dateOfBirth,
                    image = patient.image,
                    phoneNumber = patient.phoneNumber,
                    gender = patient.gender,
                    password =patient.password , 
                    type = UserType.patient
                };
                var result = await _userManager.CreateAsync(user, user.password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Patient");
                    context.Users.Add(user);


                    context.SaveChanges();
                    return user.Id;
                }
                else
                    return "";


            }
            else
                return null;

        }


        // Private Functions to make code more readable
        private int CalculateFinalPrice(int doctorPrice, int discountValue, discountType type)
        {
            if (type == discountType.percentage)
            {
                return doctorPrice - ((int)(doctorPrice * discountValue));
            }
            else
            {
                return doctorPrice - discountValue;
            }
        }

        private bool IsDiscountEligible(int discountID, string patientID, int countOfRequests, string doctorID)
        {
            if (discountID != 0)
            {
                var discount = context.Discounts.FirstOrDefault(d => d.discountID == discountID);

                if (discount != null && discount.numOfRequests == countOfRequests)
                {
                    var doctor = context.Doctors.FirstOrDefault(d => d.Id == doctorID);
                    return true;
                }
            }
            return false;
        }

        private bool IsTimeSlotBooked(string doctorID, int timeSlotID)
        {
            return context.Bookings.Any(b => b.DoctorID == doctorID && b.timeSlotID == timeSlotID && b.BookingStatus == Status.pending);
        }

    }
}
