using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProBet.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name ="Bet Time")]
        public DateTime? BetTime { get; set; }
        [Display(Name = "Bet Money")]
        public float BetMoney { get; set; }
        [Display(Name = "Win Money")]
        public float? WinMoney { get; set; }
        [Range(0,2)]
        public int Tip { get; set; }
        public int? Won { get; set; }
        [Display(Name ="Match")]
        public int MatchId { get; set; }
        public Match Match { get; set; }
        [Display(Name = "Gambler")]
        public int GamblerId { get; set; }
        public Gambler Gambler { get; set; }
    }
}
