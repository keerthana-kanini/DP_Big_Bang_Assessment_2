using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;
        public string PatientPhoneNumber { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;

        // Navigation properties
        public int PatientId { get; set; }
        public Patient ? Patient { get; set; } 

        public int DoctorId { get; set; }
        public Doctor?  Doctor { get; set; }
    }
}
