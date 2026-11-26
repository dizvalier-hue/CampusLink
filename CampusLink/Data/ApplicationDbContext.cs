using CampusLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusLink.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Fix the decimal warning for Price
            modelBuilder.Entity<Listing>()
                .Property(l => l.Price)
                .HasPrecision(18, 2);

            // 2. Create a static date so EF Core doesn't get confused
            var staticDate = new DateTime(2024, 1, 1);

            // 3. Seed the categories with the static date
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Textbooks", DisplayOrder = 1, CreatedDateTime = staticDate },
                new Category { Id = 2, Name = "Furniture", DisplayOrder = 2, CreatedDateTime = staticDate },
                new Category { Id = 3, Name = "Electronics", DisplayOrder = 3, CreatedDateTime = staticDate },
                new Category { Id = 4, Name = "Tutoring", DisplayOrder = 4, CreatedDateTime = staticDate },
                new Category { Id = 5, Name = "Other", DisplayOrder = 5, CreatedDateTime = staticDate }
            );
        }
    }
}