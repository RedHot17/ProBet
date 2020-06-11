using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBet.Models
{
    public class Gambler
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [StringLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [StringLength(20)]
        public string Nationality { get; set; }
        public int? Earnings { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPhoto { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Gambler()
        {
            FullName = String.Format("{0} {1}", FirstName, LastName);
            Earnings = 0;
        }
    }
}
