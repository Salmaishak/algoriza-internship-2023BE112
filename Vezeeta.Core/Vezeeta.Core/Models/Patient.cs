﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public class Patient :User
    {
        public List<Patient> Patients { get; set; }
    }
}
