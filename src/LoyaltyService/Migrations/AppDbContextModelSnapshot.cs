﻿// <auto-generated />
using System;
using LoyaltyService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReservationService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ReservationService.Data.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<Guid>("HotelUid")
                        .HasColumnType("uuid")
                        .HasColumnName("hotel_uid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Price")
                        .HasColumnType("integer")
                        .HasColumnName("price");

                    b.Property<int?>("Stars")
                        .HasColumnType("integer")
                        .HasColumnName("stars");

                    b.HasKey("Id")
                        .HasName("pk_hotels");

                    b.ToTable("hotels", (string)null);
                });

            modelBuilder.Entity("ReservationService.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int?>("HotelId")
                        .HasColumnType("integer")
                        .HasColumnName("hotel_id");

                    b.Property<Guid>("PaymentUid")
                        .HasColumnType("uuid")
                        .HasColumnName("payment_uid");

                    b.Property<Guid>("ReservationUid")
                        .HasColumnType("uuid")
                        .HasColumnName("reservation_uid");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_reservations");

                    b.HasIndex("HotelId")
                        .HasDatabaseName("ix_reservations_hotel_id");

                    b.ToTable("reservations", (string)null);
                });

            modelBuilder.Entity("ReservationService.Data.Entities.Reservation", b =>
                {
                    b.HasOne("ReservationService.Data.Entities.Hotel", "Hotel")
                        .WithMany("Reservations")
                        .HasForeignKey("HotelId")
                        .HasConstraintName("fk_reservations_hotels_hotel_id");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("ReservationService.Data.Entities.Hotel", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
