using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProBet.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProBet.Models
{
    public class ProBetContext : IdentityDbContext<AppUser>
    {
        public ProBetContext (DbContextOptions<ProBetContext> options)
            : base(options)
        {
        }

        public DbSet<ProBet.Models.Gambler> Gambler { get; set; }

        public DbSet<ProBet.Models.Ticket> Ticket { get; set; }

        public DbSet<ProBet.Models.Match> Match { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Ticket>()
                .HasOne<Gambler>(p => p.Gambler)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.GamblerId);
            builder.Entity<Ticket>()
                .HasOne<Match>(p => p.Match)
                .WithMany(p => p.Tickets)
                .HasForeignKey(p => p.MatchId);
        }
    }
}
