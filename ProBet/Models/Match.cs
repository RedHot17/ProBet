using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBet.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Home Team")]
        public string HomeTeam { get; set; }
        [Display(Name = "Away Team")]
        public string AwayTeam { get; set; }
        [Display(Name ="1")]
        public float HomeOdds { get; set; }
        [Display(Name ="X")]
        public float DrawOdds { get; set; }
        [Display(Name ="2")]
        public float AwayOdds { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        public string Stadium { get; set; }
        [Display(Name = "Home")]
        [Range(0,15)]
        public int? HomeGoals { get; set; }
        [Display(Name = "Away")]
        [Range(0, 15)]
        public int? AwayGoals { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
