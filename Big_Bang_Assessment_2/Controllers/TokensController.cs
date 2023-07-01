using Big_Bang_Assessment_2.DBContext;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Big_Bang_Assessment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        public IConfiguration? _configuration;
        private readonly HospitalDPContext _context;
        private const string UserRole = "Doctor";
        private const string AdminRole = "Admin";
        private const string UsersRole = "Patient";


        public TokensController(IConfiguration config, HospitalDPContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostDoctor(Doctor _userData)
        {
            if (_userData != null && _userData.Doctor_Name != null && _userData.Password != null)
            {
                var users = await GetDoctor(_userData.Doctor_Name, _userData.Password);

                if (users != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                         new Claim("Doctor_Name", users.Doctor_Name),
                        new Claim("Password",users.Password),
                        new Claim(ClaimTypes.Role, UserRole)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Doctor> GetDoctor(string Doctor_Name, string Password)
        {
            return await _context.Doctors.FirstOrDefaultAsync(u => u.Doctor_Name == Doctor_Name && u.Password == Password);
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> PostAdmin(Admin _adminData)
        {
            if (_adminData != null && _adminData.Admin_Name != null && _adminData.Admin_Password != null)
            {
                var admin = await GetAdmin(_adminData.Admin_Name, _adminData.Admin_Password);

                if (admin != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Admin_Name", admin.Admin_Name.ToString()),
                        new Claim("Admin_Password",admin.Admin_Password),
                        new Claim(ClaimTypes.Role, AdminRole)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Admin> GetAdmin(string Admin_Name, string Admin_Password)
        {
            return await _context.Admins.FirstOrDefaultAsync(u => u.Admin_Name == Admin_Name && u.Admin_Password == Admin_Password);
        }

        [HttpPost("Patient")]
        public async Task<IActionResult> PostPatient(Patient _adminData)
        {
            if (_adminData != null && _adminData.Patient_Name != null && _adminData.Password != null)
            {
                var admin = await GetPatients(_adminData.Patient_Name, _adminData.Password);

                if (admin != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Patient_Name", admin.Patient_Name.ToString()),
                        new Claim("Password",admin.Password),
                        new Claim(ClaimTypes.Role, UsersRole)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Patient> GetPatients(string Patient_Name, string Password)
        {
            return await _context.Patients.FirstOrDefaultAsync(u => u.Patient_Name == Patient_Name && u.Password == Password);
        }
    }
}

