using Microsoft.EntityFrameworkCore;
using Stations.Models;

namespace Stations.Data
{
    public class StationsDbContext : DbContext
    {
        public StationsDbContext()
        {
        }

        public StationsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Train> Trains { get; set; }
        public DbSet<SeatingClass> SeatingClasses { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<TrainSeat> TrainSeats { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CustomerCard> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeatingClass>().HasAlternateKey(x => x.Name); 
            modelBuilder.Entity<SeatingClass>().HasAlternateKey(x => x.Abbreviation);

            modelBuilder.Entity<Station>().HasAlternateKey(x => x.Name); 

            modelBuilder.Entity<Train>().HasAlternateKey(x => x.TrainNumber);

            modelBuilder.Entity<Trip>()
                .HasOne(x => x.OriginStation)
                .WithMany(os => os.TripsFrom)
                .HasForeignKey(x => x.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
               .HasOne(x => x.DestinationStation)
               .WithMany(ds => ds.TripsTo)
               .HasForeignKey(x => x.DestinationStationId)
               .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}