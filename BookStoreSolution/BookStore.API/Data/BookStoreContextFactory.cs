using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.API.Data
{
    public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
    {
        public BookStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookStoreContext>();

          
            optionsBuilder.UseSqlServer("Server=.;Database=BookStoreDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new BookStoreContext(optionsBuilder.Options);
        }
    }
}