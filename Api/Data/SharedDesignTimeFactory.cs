using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlazorApp.Api.Data
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<AdminContext>
    {
        public AdminContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AdminContext>();

            return new AdminContext(optionsBuilder.Options);
        }
    }
}
