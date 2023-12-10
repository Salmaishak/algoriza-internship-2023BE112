using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Models;

namespace Vezeeta.Core.DTOs
{
    public class PatientDTO
    {
        public string fname { get; set; }
        [Required]
        public string lname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter," +
            " one number, and one special character")]
        public string password { get; set; }
        public string? image { get; set; }

        public string phoneNumber
        { get; set; }

        [Required]
        public Gender gender { get; set; }
        [Required]

        public DateTime dateOfBirth { get; set; }
    }
}
