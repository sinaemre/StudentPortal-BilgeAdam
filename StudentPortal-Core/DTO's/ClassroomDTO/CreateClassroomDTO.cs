using StudentPortal_Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.ClassroomDTO
{
    public class CreateClassroomDTO
    {
        [Display(Name = "Sınıf Adı")]
        public string? ClassroomName { get; set; }
        
        [Display(Name = "Sınıf Açıklaması")]
        public string? ClassroomDescription { get; set; }

        [Display(Name = "Öğretmen")]
        public int? TeacherId { get; set; }

        public List<Teacher>? Teachers { get; set; }
    }
}
