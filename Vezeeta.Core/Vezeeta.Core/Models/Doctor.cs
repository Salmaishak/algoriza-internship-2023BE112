﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public class Doctor : User
    {
            public int specializationID { get; set; }
        public Specialization Specialization { get; set; }
            public List<Appointment> Doctors_Appointments { get; set; }
    }
}
