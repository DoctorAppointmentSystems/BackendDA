using DoctorAppointment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Specialty> Specialties { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DoctorAvailability>()
        .HasKey(d => d.AvailabilityId);

        modelBuilder.Entity<DoctorAvailability>()
            .HasOne(d => d.Doctor)
            .WithMany(d => d.Availabilities)
            .HasForeignKey(d => d.DoctorId);
    }
}