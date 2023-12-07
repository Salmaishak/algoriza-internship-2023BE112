using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.DTOs
{
    public class AddDoctorDTO
    {
        //add string image, string fname, string lname, string email, string phone, string specialization,
        ///string gender, DateTime dateofBirth

        [Required]
       public string image {  get; set; }
        [Required]
        public string fname { get; set; }
        [Required]
        public string lname { get; set; }
        [Required]
        public string email {  get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public int specializationID {  get; set; }
        [Required]

        public float price { get; set; }
        public Gender gender {  get; set; }
        public DateTime dateOfBirth { get; set; }


    }
}
