
using HMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Guest>()
                .HasIndex(g => g.PersonalNumber)
                .IsUnique();

            modelBuilder.Entity<Guest>()
                .HasIndex(g => g.Email)
                .IsUnique();

            modelBuilder.Entity<Guest>()
                .HasIndex(g => g.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Manager>()
                .HasIndex(m => m.PersonalNumber)
                .IsUnique();

            modelBuilder.Entity<Manager>()
                .HasIndex(m => m.Email)
                .IsUnique();


            modelBuilder.Entity<ReservationRoom>()
                .HasKey(rr => new { rr.ReservationId, rr.RoomId });

            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Reservation)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Room)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
