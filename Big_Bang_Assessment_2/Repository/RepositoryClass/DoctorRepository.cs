using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Big_Bang_Assessment_2.DBContext;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Repositories
{
    public class DoctorRepository : IDoctor
    {
        private readonly HospitalDPContext _context;

        public DoctorRepository(HospitalDPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _context.Doctors
                .Include(x => x.Patients)
                .Include(x => x.Appointments)
                .ToListAsync();
        }

        public async Task<Doctor> GetDoctor(int id)
        {
            return await _context.Doctors
                .Include(x => x.Patients)
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Doctor_Id == id);
        }

        public async Task AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctor(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoctorExists(int id)
        {
            return await _context.Doctors.AnyAsync(e => e.Doctor_Id == id);
        }
    }
}
