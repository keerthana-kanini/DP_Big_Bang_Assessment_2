using ClassLibrary.Models;

namespace Big_Bang_Assessment_2.Repository.Interface
{
    public interface IAdmin
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);
        Task<Admin> AddAdmin(Admin admin);
        Task<bool> UpdateAdmin(int id, Admin admin);
        Task<bool> DeleteAdmin(int id);
        Task<IEnumerable<Doctor>> GetDoctorRequests();
        Task<bool> ApproveDoctorRequest(int id);
    }
}
