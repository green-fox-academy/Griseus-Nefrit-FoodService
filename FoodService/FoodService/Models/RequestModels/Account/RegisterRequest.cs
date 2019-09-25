using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Models.RequestModels.Account
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [MinLength(5, ErrorMessage = "{0} must be at least {1} characters long.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
