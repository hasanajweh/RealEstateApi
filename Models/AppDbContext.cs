using Microsoft.EntityFrameworkCore;

namespace PalsoftRealEstate.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<AdvertiseRequest> AdvertiseRequests { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many relationship between Property and AdvertiseRequest
            modelBuilder.Entity<Property>()
                .HasMany(p => p.AdvertiseRequests)
                .WithOne(ar => ar.Property)
                .HasForeignKey(ar => ar.PropertyId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
