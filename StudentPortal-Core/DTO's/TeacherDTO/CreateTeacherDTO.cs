using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.TeacherDTO
{
    public class CreateTeacherDTO
    {
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string? LastName { get; set; }
        
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
    }
}
