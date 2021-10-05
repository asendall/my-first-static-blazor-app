using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Api.Data
{
    public class AdminContext : DbContext
    {
        public virtual DbSet<AppCustomer> Customers { get; set; }

        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB; database = GAR.Admin; trusted_connection = yes;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
    }
}
}
