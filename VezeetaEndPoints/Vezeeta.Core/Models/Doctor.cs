using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public class Doctor : User
    {
        public string doctorid { get; set; }
        public float price { get; set; }
            public int specializationID { get; set; }
            public Specialization Specialization { get; set; }
            public List<Appointment> Doctors_Appointments { get; set; }
    }
}
