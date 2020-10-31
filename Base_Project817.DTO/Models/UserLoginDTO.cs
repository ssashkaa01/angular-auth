using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Base_Project817.DTO.Models
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Emai is required field")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required field")]
        public string Password { get; set; }
    }
}
