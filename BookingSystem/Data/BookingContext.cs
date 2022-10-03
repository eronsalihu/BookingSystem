using BookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        public DbSet<GuestHouse> GuestHouses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<Book> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomAmenity>().HasNoKey();
            modelBuilder.Entity<RoomAmenity>()
                .Property(e => e.Amenities)
                .HasConversion(
                e => e.ToString(),
                e => (AmenitiesEnum)Enum.Parse(typeof(AmenitiesEnum), e));

            //GuestHouse
            modelBuilder.Entity<GuestHouse>().HasKey(e => e.Id);
            modelBuilder.Entity<GuestHouse>().Property(e => e.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<GuestHouse>().Property(e => e.Description).IsRequired().HasMaxLength(60);

            //Room
            modelBuilder.Entity<Room>().HasKey(e => e.Id);
            modelBuilder.Entity<Room>().Property(e => e.Name).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Room>().Property(e => e.Description).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Room>().Property(e => e.Image).IsRequired();
            modelBuilder.Entity<Room>().Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Room>().Property(e => e.Day).IsRequired();
            modelBuilder.Entity<Room>().Property(e => e.NumberOfBeds).IsRequired();



            //Book
            modelBuilder.Entity<Book>().HasKey(e => e.Id);
            modelBuilder.Entity<Book>().Property(e => e.BookFrom).IsRequired();
            modelBuilder.Entity<Book>().Property(e => e.BookTo).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
