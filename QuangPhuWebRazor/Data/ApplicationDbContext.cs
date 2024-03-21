using Microsoft.EntityFrameworkCore;
using QuangPhuWebRazor.Models;

namespace QuangPhuWebRazor.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Book", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Bag", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Note", DisplayOrder = 3 }

                );
        }
    }
}
