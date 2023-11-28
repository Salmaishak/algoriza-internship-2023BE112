using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public enum Gender { female, male}
    public enum UserType { admin, doctor, patient }
    public class User
    {
        public int userId {  get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string image { get; set; }
        public string phoneNumber
        { get; set; }
         public UserType type { get; set; }

        public Gender gender { get; set; }


    }
}
