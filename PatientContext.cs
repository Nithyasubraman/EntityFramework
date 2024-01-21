using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.EntityFrameworkCore.Extensions;


namespace PatientManagementSystem
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public object? Patient { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .Build();


            string connectionString = configuration.GetConnectionString("DefaultConnection");


            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientID);
                entity.Property(e => e.PatientName);
                entity.Property(e => e.PatientAge);
                entity.Property(e => e.PatientEmail);
                entity.Property(e => e.PatientBloodGroup).IsRequired();
                entity.Property(e => e.PatientLocation);
            });
        }
    }
}