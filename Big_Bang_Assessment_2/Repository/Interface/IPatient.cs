using ClassLibrary.Models;

namespace Big_Bang_Assessment_2.Repository.Interface
{
    public interface IPatient
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatient(int id);
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
        Task<bool> PatientExists(int id);
    }
}
