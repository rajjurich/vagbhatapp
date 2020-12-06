using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.Data.Core
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext(DbContextOptions<EntitiesContext> options)
           : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Treatment> Treatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>().Property(p => p.AddressId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Address>().Property(p => p.PatientId).IsRequired();

            modelBuilder.Entity<Appointment>().Property(p => p.AppointmentId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Appointment>().Property(p => p.PatientId).IsRequired();



            modelBuilder.Entity<Patient>().Property(p => p.PatientId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Patient>().Property(p => p.PatientName).IsRequired();
            modelBuilder.Entity<Patient>().HasIndex(i => i.PatientName);
            modelBuilder.Entity<Patient>().HasIndex(i => i.MobileNumber);
            modelBuilder.Entity<Patient>().Property(p => p.MobileNumber).IsRequired();
            modelBuilder.Entity<Patient>().Property(p => p.MobileNumber).HasMaxLength(15);
            modelBuilder.Entity<Patient>().Property(p => p.Gender).HasMaxLength(10);
            modelBuilder.Entity<Patient>().Property(p => p.Gender).IsRequired();
            modelBuilder.Entity<Patient>().Property(p => p.PatientHistory).IsRequired();

            modelBuilder.Entity<Appointment>().HasIndex(i => i.AppointmentDate);

            modelBuilder.Entity<Treatment>().HasOne(o => o.Appointment)
               .WithOne(o => o.Treatment)
               .IsRequired()
               .HasForeignKey<Treatment>(fk => fk.TreatmentId);
        }
    }
}
