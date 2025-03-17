
using Dot.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Dot.DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category { id = 1, Name = "C#", DisplayOrder = 1 },
                new Category { id = 2, Name = "ASP.NET Core", DisplayOrder = 2 },
                new Category { id = 3, Name = "Entity Framework Core", DisplayOrder = 3 },
                new Category { id = 4, Name = "Azure", DisplayOrder = 4 }
            );
        }

    }
}
