using Big_Bang_Assessment_2.Repository.Interface;
using Big_Bang_Assessment_2.Repository.RepositoryClass;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Big_Bang_Assessment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentRepository;

        public AppointmentController(IAppointment appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var appointments = await _appointmentRepository.GetAppointments();
            return Ok(appointments);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
                return NotFound();

            return appointment;
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            var addedAppointment = await _appointmentRepository.AddAppointment(appointment);
            return CreatedAtAction("GetAppointment", new { id = addedAppointment.AppointmentId }, addedAppointment);
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            var result = await _appointmentRepository.UpdateAppointment(id, appointment);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentRepository.DeleteAppointment(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}
