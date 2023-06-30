using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary.Models;
using ClassLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Big_Bang_Assessment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctorRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DoctorController(IDoctor doctorRepository, IWebHostEnvironment hostEnvironment)
        {
            _doctorRepository = doctorRepository;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await _doctorRepository.GetDoctors();
            return Ok(doctors);
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _doctorRepository.GetDoctor(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Doctor_Id)
            {
                return BadRequest();
            }

            try
            {
                await _doctorRepository.UpdateDoctor(doctor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _doctorRepository.DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> PostDoctor(IFormFile imageFile, [FromForm] Doctor doctor)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                return BadRequest("Image file is required.");
            }

            var imageData = await ConvertImageToByteArray(imageFile);
            doctor.ImageData = imageData;

            await _doctorRepository.AddDoctor(doctor);

            return CreatedAtAction("GetDoctor", new { id = doctor.Doctor_Id }, doctor);
        }

        private async Task<byte[]> ConvertImageToByteArray(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _doctorRepository.GetDoctor(id);
            if (doctor == null)
            {
                return NotFound();
            }

            await _doctorRepository.DeleteDoctor(doctor);

            return NoContent();
        }
    }
}