using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Big_Bang_Assessment_2.DBContext
{
    public class HospitalDPContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Appointment> Appointments { get; set; }    

        public HospitalDPContext(DbContextOptions<HospitalDPContext> options) : base(options)
        {

        }
    }
}