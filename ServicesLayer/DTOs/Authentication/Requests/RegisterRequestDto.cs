using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.DTOs.Authentication.Requests
{
    public class RegisterRequestDto
    {
        [Required, StringLength(10, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [StringLength(30, ErrorMessage = "Email length cannot exceed 30 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }

        public string? Image { get; set; }
    }
}
