﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoApi;

#nullable disable

namespace TodoApi.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20241021193502_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("Salong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Salongs", (string)null);
                });

            modelBuilder.Entity("TodoApi.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);
                });

            modelBuilder.Entity("TodoApi.Models.Reserve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReservationsCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("VisningId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VisningsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VisningId");

                    b.ToTable("Reservs", (string)null);
                });

            modelBuilder.Entity("TodoApi.Models.Visning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReservedSeats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SalongId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("SalongId");

                    b.ToTable("Visnings", (string)null);
                });

            modelBuilder.Entity("TodoApi.Models.Reserve", b =>
                {
                    b.HasOne("TodoApi.Models.Visning", "Visning")
                        .WithMany()
                        .HasForeignKey("VisningId");

                    b.Navigation("Visning");
                });

            modelBuilder.Entity("TodoApi.Models.Visning", b =>
                {
                    b.HasOne("TodoApi.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salong", "Salong")
                        .WithMany()
                        .HasForeignKey("SalongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Salong");
                });
#pragma warning restore 612, 618
        }
    }
}
