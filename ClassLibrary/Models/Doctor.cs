using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models
{
    public class Doctor
    {
        [Key]
        public int Doctor_Id { get; set; }
        public string Doctor_Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Doctor_Email { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string Contact_No { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public byte[] ImageData { get; set; } = Array.Empty<byte>();

        // Navigation property for patients
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

        // Navigation property for appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
