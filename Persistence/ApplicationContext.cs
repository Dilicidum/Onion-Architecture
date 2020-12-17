using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor_Schedule> Doctor_Schedules { get; set; }
        public DbSet<Patient_Visiting> Patient_Visitings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Doctor
            
            builder.Entity<Doctor>()
                .HasMany(c => c.Appointments)
                .WithOne(c => c.Doctor)
                .HasForeignKey(p => p.DoctorId);
            builder.Entity<Doctor>()
                .Property(p => p.Post)
                .HasConversion<string>();
            builder.Entity<Doctor>()
                .Property(p => p.Specialization)
                .HasConversion<string>();
            builder.Entity<Doctor>()
                .HasMany(c => c.Doctor_Schedules)
                .WithOne(p => p.Doctor)
                .HasForeignKey(k => k.DoctorId);
                

            //Patient_Visiting
            builder.Entity<Patient_Visiting>()
                .HasKey(c => c.AppointmentId);
            builder.Entity<Patient_Visiting>()
                .Property(c => c.Specialization)
                .HasColumnName("Specialization");
            builder.Entity<Patient_Visiting>()
                .HasOne(c => c.Patient)
                .WithMany(c => c.Visitings)
                .HasForeignKey(c => c.PatientId);
            builder.Entity<Patient_Visiting>()
                .HasOne(pv => pv.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(pv => pv.DoctorId);

            builder.Entity<Patient>()
                .Ignore(c => c.FullName);

            //doctor schedule
            builder.Entity<Doctor_Schedule>()
                .HasKey(k => k.Id);
                
            base.OnModelCreating(builder);
        }


    }
}
