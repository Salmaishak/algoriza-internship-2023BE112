using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public class TimeSlot
    {
        [Key]
        public int SlotId { get; set; }
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }

        public TimeSpan Time { get; set; }
    }
}
