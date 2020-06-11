using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBet.ViewModels
{
    public class GamblerViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public int? Earnings { get; set; }
        public IFormFile ProfilePictureVM { get; set; }
        public IFormFile CoverPhotoVM { get; set; }
    }
}
