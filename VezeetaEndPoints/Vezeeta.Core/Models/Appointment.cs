using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public enum DayOfWeek { Saturday, Sunday, Monday, Tuesday, Wednesday,Thursday, Friday}
    public class Appointment
    {
        // appointment ID
        public int Id { get; set; }
        //doctor 
        public string doctorID { get; set; }
        //doctor object for creating the realtionship 
        public Doctor doctor { get; set; }
         

        // day of the appointment
        public DayOfWeek day {  get; set; }
         // time slots for that one day
        List<TimeSlot> timeSlots { get; set; }


    }
}
