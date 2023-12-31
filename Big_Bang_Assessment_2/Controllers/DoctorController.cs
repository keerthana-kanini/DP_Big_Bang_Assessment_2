﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary.Models;
using ClassLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> UpdateDoctor(int id, [FromForm] Doctor doctor, IFormFile imageFile)
        {
            if (id != doctor.Doctor_Id)
            {
                return BadRequest();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var imageData = await ConvertImageToByteArray(imageFile);
                doctor.ImageData = imageData;
            }

            try
            {
                await _doctorRepository.UpdateDoctor(doctor);
            }
            catch
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


        // POST: api/Doctor
        [HttpPost]
        public async Task<IActionResult> CreateDoctorRequest([FromForm] Doctor doctor, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                return BadRequest("Image file is required.");
            }

            var imageData = await ConvertImageToByteArray(imageFile);
            doctor.ImageData = imageData;

            doctor.Status = "Pending"; // Set the status of the doctor to "Pending"

            await _doctorRepository.AddDoctor(doctor);

            // Send approval request to the admin
            SendApprovalRequestToAdmin(doctor.Doctor_Id);

            return CreatedAtAction("GetDoctor", new { id = doctor.Doctor_Id }, doctor);
        }

        private void SendApprovalRequestToAdmin(int doctorId)
        {
            // Implement the logic to send an approval request to the admin
        }
        private async Task<byte[]> ConvertImageToByteArray(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

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

        // GET: api/Doctor/BySpecialization?specialization=xxx
        [HttpGet("BySpecialization")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = await _doctorRepository.GetDoctors();
            var filteredDoctors = doctors.Where(d => d.Specialization != null && d.Specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase));
            return Ok(filteredDoctors);
        }


        // GET: api/Doctor/ApprovedDoctors
        [HttpGet("ApprovedDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetApprovedDoctors()
        {
            var approvedDoctors = await _doctorRepository.GetDoctors();
            var filteredDoctors = approvedDoctors.Where(d => d.Status == "Approved");
            return Ok(filteredDoctors);
        }


    }
}