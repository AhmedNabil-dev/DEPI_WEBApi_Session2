using DEPI_EF_Session14.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DEPI_WebApi_Session2.Infrestraction.Data
{
    public class ApplicationDBContext : DbContext
    {
       public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=>  optionsBuilder.UseSqlServer("server=.;Database=DEPI_WEb_APi;Trusted_Connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Doctor> Doctor { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
