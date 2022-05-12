using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Reservation_vols_version2_withEF.Models;

namespace Reservation_vols_version3_withIdentity_démo.Models
{
    public partial class Reservation_volsContext : DbContext
    {
        public Reservation_volsContext()
        {
        }

        public Reservation_volsContext(DbContextOptions<Reservation_volsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Reservation_vols;User Id=postgres;Password=b6364b55ba47e8edbbfe4e2bf01e2256;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("airports");

                entity.HasIndex(e => e.Address, "airports_address_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "airports_name_key")
                    .IsUnique();

                entity.Property(e => e.Airportid).HasColumnName("airportid");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Isdeleted)
                    .HasColumnName("isdeleted")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.Phonenumber, "clients_phonenumber_key")
                    .IsUnique();

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("birthdate");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Phonenumber).HasColumnName("phonenumber");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("flights");

                entity.Property(e => e.Flightid).HasColumnName("flightid");

                entity.Property(e => e.AirportArrivalId).HasColumnName("airport_arrival_id");

                entity.Property(e => e.AirportDepartureId).HasColumnName("airport_departure_id");

                entity.Property(e => e.DateArrival)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_arrival");

                entity.Property(e => e.DateDeparture)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_departure");

                entity.Property(e => e.Isopen).HasColumnName("isopen");

                entity.HasOne(d => d.AirportArrival)
                    .WithMany(p => p.FlightAirportArrivals)
                    .HasForeignKey(d => d.AirportArrivalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_arrival_airport");

                entity.HasOne(d => d.AirportDeparture)
                    .WithMany(p => p.FlightAirportDepartures)
                    .HasForeignKey(d => d.AirportDepartureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_departure_airport");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("tickets");

                entity.HasIndex(e => new { e.FlightId, e.PassengerId }, "unique_passenger")
                    .IsUnique();

                entity.Property(e => e.Ticketid).HasColumnName("ticketid");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.FlightId).HasColumnName("flight_id");

                entity.Property(e => e.Isconfirmed).HasColumnName("isconfirmed");

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.TicketClients)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_flight");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.TicketPassengers)
                    .HasForeignKey(d => d.PassengerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_passenger");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
