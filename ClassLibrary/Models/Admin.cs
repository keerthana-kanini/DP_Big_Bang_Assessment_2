using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; } 
        public string Admin_Name { get; set; } = string.Empty;

        public string Admin_Email { get; set; } = string.Empty;
        public string Admin_Password { get; set; } = string.Empty;
    }
}
