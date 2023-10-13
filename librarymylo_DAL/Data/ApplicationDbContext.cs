using librarymylo_BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace librarymylo_DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PersonCategory> PersonCategories { get; set; }
    }
}