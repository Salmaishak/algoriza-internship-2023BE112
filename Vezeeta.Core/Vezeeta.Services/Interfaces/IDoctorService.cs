using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Services.Interfaces
{
    public interface IDoctorService 
    {
        public bool login (string email, string password);
    }
}
