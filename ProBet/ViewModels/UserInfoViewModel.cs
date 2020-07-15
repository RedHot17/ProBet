using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBet.ViewModels
{
    public class UserInfoViewModel
    {
        [Display(Name = "User")]
        public string UserDetails { get; set; }
        public string Role { get; set; }
        [Display(Name = "User Id")]
        public string Id { get; set; }
        [Display(Name = "Password Hash")]
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
