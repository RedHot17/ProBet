using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProBet.Models
{
    public class AppUser:IdentityUser
    {
        public string Role { get; set; }
        public int? GamblerId { get; set; }
        [ForeignKey("GamblerId")]
        public Gambler Gambler { get; set; }
    }
}
