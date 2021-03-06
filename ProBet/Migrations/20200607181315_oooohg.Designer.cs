﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProBet.Models;

namespace ProBet.Migrations
{
    [DbContext(typeof(ProBetContext))]
    [Migration("20200607181315_oooohg")]
    partial class oooohg
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProBet.Models.Gambler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Earnings")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gambler");
                });

            modelBuilder.Entity("ProBet.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AwayGoals")
                        .HasColumnType("int");

                    b.Property<float>("AwayOdds")
                        .HasColumnType("real");

                    b.Property<string>("AwayTeam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("DrawOdds")
                        .HasColumnType("real");

                    b.Property<int?>("HomeGoals")
                        .HasColumnType("int");

                    b.Property<float>("HomeOdds")
                        .HasColumnType("real");

                    b.Property<string>("HomeTeam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stadium")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("ProBet.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BetMoney")
                        .HasColumnType("real");

                    b.Property<DateTime?>("BetTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GamblerId")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.Property<float?>("WinMoney")
                        .HasColumnType("real");

                    b.Property<bool>("Won")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GamblerId");

                    b.HasIndex("MatchId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("ProBet.Models.Ticket", b =>
                {
                    b.HasOne("ProBet.Models.Gambler", "Gambler")
                        .WithMany("Tickets")
                        .HasForeignKey("GamblerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProBet.Models.Match", "Match")
                        .WithMany("Tickets")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
