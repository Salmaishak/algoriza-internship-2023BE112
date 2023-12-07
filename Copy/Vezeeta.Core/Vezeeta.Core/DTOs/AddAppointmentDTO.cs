﻿using DayOfWeek = Vezeeta.Core.Models.DayOfWeek;

namespace Vezeeta.Presentation.API.Models
{
    public class AddAppointmentDTO
    {
       public int doctorId { get; set; }
        public float price { get; set; }

        public IDictionary<DayOfWeek, List<TimeSpan>> times { get; set; }


    }
}
