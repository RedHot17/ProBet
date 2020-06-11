using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProBet.Models;

namespace ProBet.Data
{
    public class ProBetContext : DbContext
    {
        public ProBetContext (DbContextOptions<ProBetContext> options)
            : base(options)
        {
        }

        public DbSet<ProBet.Models.Gambler> Gambler { get; set; }

        public DbSet<ProBet.Models.Ticket> Ticket { get; set; }

        public DbSet<ProBet.Models.Match> Match { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne<Gambler>(p => p.Gambler)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.GamblerId);
            modelBuilder.Entity<Ticket>()
                .HasOne<Match>(p => p.Match)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.MatchId);
        }
    }
}
