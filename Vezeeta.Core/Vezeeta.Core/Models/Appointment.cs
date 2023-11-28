using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public enum DayOfWeek { Saturday, Sunday, Monday, Tuesday, Wednesday,Thursday, Friday}
    public enum Appointment_Status { pending, completed, canceled}
    public class Appointment
    {

        public TimeOnly timeID {  get; set; }
        public DateOnly date { get; set; }
        public DayOfWeek dayOfWeek { get; set;}

        public int discountID { get; set; }

        public Discount discount { get; set; }

        public int doctorID { get; set; }

        public Doctor doctor { get; set; }
        public int patientID { get; set; }

        public Patient patient { get; set; }

        public float price { get; set; }

        public Appointment_Status status { get; set; }



    }
}
