using Big_Bang_Assessment_2.DBContext;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Big_Bang_Assessment_2.Repository.RepositoryClass
{
    public class AdminRepository
    {
        private readonly HospitalDPContext _context;

        public AdminRepository(HospitalDPContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdminById(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> UpdateAdmin(int id, Admin admin)
        {
            if (id != admin.Admin_Id)
            {
                return false;
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return false;
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Admin_Id == id);
        }
    }

}
