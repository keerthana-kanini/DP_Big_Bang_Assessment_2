using Big_Bang_Assessment_2.DBContext;
using ClassLibrary.Models;

namespace Big_Bang_Assessment_2.Repository.RepositoryClass
{
    public class AppointmentRepository
    {
        private readonly HospitalDPContext _context;

        public AppointmentRepository(HospitalDPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _context.Appointments.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> UpdateAppointment(int id, Appointment appointment)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
                return false;

            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.Description = appointment.Description;
            existingAppointment.PatientName = appointment.PatientName;
            existingAppointment.PatientPhoneNumber = appointment.PatientPhoneNumber;
            existingAppointment.PatientEmail = appointment.PatientEmail;
            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.DoctorId = appointment.DoctorId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

