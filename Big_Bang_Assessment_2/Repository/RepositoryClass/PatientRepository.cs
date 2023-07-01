using Big_Bang_Assessment_2.DBContext;
using Big_Bang_Assessment_2.Repository.Interface;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Big_Bang_Assessment_2.Repository.RepositoryClass
{
    public class PatientRepository : IPatient
    {
        private readonly HospitalDPContext _context;

        public PatientRepository(HospitalDPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _context.Patients
                .Include(x => x.Doctor)
                .Include(x => x.Appointments)
                .ToListAsync();
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await _context.Patients
                .Include(x => x.Doctor)
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Patient_Id == id);
        }

        public async Task AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatient(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatient(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PatientExists(int id)
        {
            return await _context.Patients.AnyAsync(e => e.Patient_Id == id);
        }
    }
}
