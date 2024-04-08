using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuangPhu.Models;
using System.Data;

namespace QuangPhu.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  {  }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Book", DisplayOrder=1},
                new Category { Id = 2, Name = "Book", DisplayOrder=2},
                new Category { Id = 3, Name = "Book", DisplayOrder=3}
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Quang Phu",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas consectetur fermentum justo quis tincidunt. Maecenas blandit quis nibh a malesuada. Suspendisse sed lacus massa.",
                    ISBN = "DH2354",
                    listPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Phuc Duc",
                    Description = "Donec semper eleifend odio, sed dictum lectus semper quis. In lacinia eu lectus vitae pharetra. Nulla sollicitudin ipsum quis ultrices vestibulum.",
                    ISBN = "SJ8923",
                    listPrice = 50,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Sunset Sunrise",
                    Author = "Tran Luong",
                    Description = "Nullam hendrerit ut velit sed eleifend. Duis vulputate risus nec velit finibus, porta porta felis lacinia. Ut condimentum non ex sed convallis. ",
                    ISBN = "HE335345",
                    listPrice = 55,
                    Price = 40,
                    Price50 = 35,
                    Price100 = 30,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Title = "Maecenas",
                    Author = "Van Lam",
                    Description = "Maecenas sollicitudin, lorem ac porta aliquet, tortor quam cursus sapien, id aliquam nisl diam bibendum nulla. Suspendisse cursus magna ut euismod egestas.",
                    ISBN = "SF32544",
                    listPrice = 49,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Title = "Fusce vitae",
                    Author = "Truc",
                    Description = "Fusce vitae pellentesque felis. Etiam ipsum nisl, mattis sed ligula nec, lobortis finibus justo. Etiam accumsan magna non nisl aliquet rutrum.",
                    ISBN = "HE345346",
                    listPrice = 80,
                    Price = 60,
                    Price50 = 55,
                    Price100 = 50,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Title = "Fortune of Time",
                    Author = "Pellentesque",
                    Description = " Pellentesque urna ipsum, lacinia nec dignissim vitae, condimentum a sapien. Nam nisl sem, feugiat at varius et, vehicula bibendum leo.",
                    ISBN = "IH3438767",
                    listPrice = 70,
                    Price = 50,
                    Price50 = 45,
                    Price100 = 40,
                    CategoryId = 3,
                    ImageUrl = ""
                }
                );
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 2,
                    Name = "ABC Company",
                    StreetAdress = "123 Main Street",
                    City = "New York City",
                    State = "New York",
                    PostalCode = "12345",
                    PhoneNumber = "555-123-4567"
                },
                new Company
                {
                    Id = 3,
                    Name = "XYZ Company",
                    StreetAdress = "456 Elm Street",
                    City = "Los Angeles",
                    State = "California",
                    PostalCode = "67890",
                    PhoneNumber = "555-987-6543"
                },
                new Company
                {
                    Id = 4,
                    Name = "123 Company",
                    StreetAdress = "789 Oak Street",
                    City = "Chicago",
                    State = "Illinois",
                    PostalCode = "54321",
                    PhoneNumber = "555-456-7890"
                }
            );
        }
    }
}
