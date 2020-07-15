using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProBet.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("Gambler");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Gambler"));
            }

            AppUser user = await UserManager.FindByEmailAsync("admin@probet.com");
            if (user == null)
            {
                var User = new AppUser();
                User.Email = "admin@probet.com";
                User.UserName = "admin@probet.com";
                User.Role = "Admin";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin      
                if (chkUser.Succeeded)
                {
                    var result1 = await UserManager.AddToRoleAsync(User, "Admin");
                }
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProBetContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ProBetContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                // Look for any movies.
                if (context.Gambler.Any() || context.Match.Any())
                {
                    return;   // DB has been seeded
                }

                /*context.Gambler.AddRange(
                    new Gambler { FirstName = "Petar", LastName = "Savevski",FullName="Petar Savevski", Nationality = "Macedonia" },
                    new Gambler { FirstName = "Savce", LastName = "Savevski", FullName = "Savce Savevski", Nationality = "USA" },
                    new Gambler { FirstName = "Petar", LastName = "Ilievski", FullName = "Petar Ilievski", Nationality = "Germany" },
                    new Gambler { FirstName = "Davor", LastName = "Milenkovski", FullName = "Davor Milenkovski", Nationality = "Greece" },
                    new Gambler { FirstName = "Danilo", LastName = "Monev", FullName = "Danilo Monev", Nationality = "China" },
                    new Gambler { FirstName = "Nikola", LastName = "Pensov", FullName = "Nikola Pensov", Nationality = "Serbia" },
                    new Gambler { FirstName = "Darko", LastName = "Ancev", FullName = "Darko Ancev", Nationality = "Cuba" },
                    new Gambler { FirstName = "Filip", LastName = "Mitovski", FullName = "Filip Mitovski", Nationality = "Nigeria" }
                );
                context.SaveChanges();*/

                context.Match.AddRange(
                    new Match
                    {
                        HomeTeam = "Barcelona",
                        AwayTeam = "Real Madrid",
                        HomeOdds = (float)1.50,
                        DrawOdds = (float)3.1,
                        AwayOdds = (float)5.25,
                        StartTime = DateTime.Parse("2020-11-7"),
                        Stadium = "Camp Nou"
                    },
                    new Match
                    {
                        HomeTeam = "Rudar",
                        AwayTeam = "Skendija",
                        HomeOdds = (float)1.8,
                        DrawOdds = (float)2.95,
                        AwayOdds = (float)4.25,
                        StartTime = DateTime.Parse("2020-10-7"),
                        Stadium = "Probistip City Stadium"
                    },
                    new Match
                    {
                        HomeTeam = "Crvena Zvezda",
                        AwayTeam = "Partizan",
                        HomeOdds = (float)2.50,
                        DrawOdds = (float)2.8,
                        AwayOdds = (float)3.1,
                        StartTime = DateTime.Parse("2020-9-7"),
                        Stadium = "Marakana"
                    },
                    new Match
                    {
                        HomeTeam = "Liverpool",
                        AwayTeam = "Manchester United",
                        HomeOdds = (float)2.1,
                        DrawOdds = (float)3.15,
                        AwayOdds = (float)2.95,
                        StartTime = DateTime.Parse("2020-9-17"),
                        Stadium = "Anfield"
                    },
                    new Match
                    {
                        HomeTeam = "Bayern Munich",
                        AwayTeam = "RB Leipzig",
                        HomeOdds = (float)1.65,
                        DrawOdds = (float)4.05,
                        AwayOdds = (float)3.5,
                        StartTime = DateTime.Parse("2020-8-27"),
                        Stadium = "Allianz Arena"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
