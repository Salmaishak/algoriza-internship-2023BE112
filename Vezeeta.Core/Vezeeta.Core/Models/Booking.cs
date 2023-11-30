using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public enum Status { pending, completed, canceled }
    public class Booking
    {
        public int BookingID { get; set; }
        public int patientID { get; set; }
        public User Patient
        { get; set; }

        public int DoctorID { get; set; }
        public Doctor Doctor
        { get; set; }

        public DayOfWeek Day { get; set; }

public int appointmentID { get; set; }
        public Appointment appointment {  get; set; }
        public Status BookingStatus { get; set; }



    }
}
