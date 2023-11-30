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

        public int discountId { get; set; }
        public Discount discount { get; set; }

        // final price after discount is to be calculated here 
        public float finalPrice { get; set; }

        public int timeSlotID { get; set; }
        public TimeSlot timeslot {  get; set; }
        public Status BookingStatus { get; set; }



    }
}
