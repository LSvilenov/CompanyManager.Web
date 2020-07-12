using CompanyManager.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<OfficeEmployee> OfficeEmployees { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Office>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Offices)
                .HasForeignKey(o => o.CompanyId);

            builder
                .Entity<Document>()
                .HasOne(d => d.Office)
                .WithMany(o => o.Documents)
                .HasForeignKey(d => d.OfficeId);

            builder
                .Entity<OfficeEmployee>()
                .HasKey(oe => new { oe.OfficeId, oe.EmployeeId });

            builder
                .Entity<OfficeEmployee>()
                .HasOne(oe => oe.Office)
                .WithMany(o => o.OfficeEmployees)
                .HasForeignKey(oe => oe.OfficeId);

            builder
                .Entity<OfficeEmployee>()
                .HasOne(oe => oe.Employee)
                .WithMany(e => e.OfficeEmployees)
                .HasForeignKey(oe => oe.EmployeeId);

            builder
                .Entity<City>()
                .HasOne(ct => ct.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(ct => ct.CountryId);

            builder
                .Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
