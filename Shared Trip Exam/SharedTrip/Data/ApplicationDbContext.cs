namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrips> UsersTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTrips>().HasKey(ut => new { ut.UserId, ut.TripId });

            modelBuilder.Entity<UserTrips>().HasOne(ut => ut.User).WithMany(u => u.Trips).HasForeignKey(ut => ut.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserTrips>().HasOne(ut => ut.Trip).WithMany(t => t.Users).HasForeignKey(ut => ut.TripId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
