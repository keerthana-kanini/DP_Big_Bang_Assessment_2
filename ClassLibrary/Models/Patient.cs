using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Patient
    {
        [Key]
        public int Patient_Id { get; set; }
        public string Patient_Name { get; set; } = string.Empty;
        public string Disease { get; set; } = string.Empty;
        public string Disease_Description { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string Patient_No { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Doctor? Doctor { get; set; }





    }
}
