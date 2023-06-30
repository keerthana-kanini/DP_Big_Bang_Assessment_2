using ClassLibrary.Models;

namespace Big_Bang_Assessment_2.Repository.Interface
{
    public interface IAppointment
    {
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<Appointment> GetAppointmentById(int id);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(int id, Appointment appointment);
        Task<bool> DeleteAppointment(int id);
    }
}
