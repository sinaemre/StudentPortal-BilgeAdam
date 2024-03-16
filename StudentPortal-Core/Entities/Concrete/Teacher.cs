using StudentPortal_Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.Entities.Concrete
{
    public class Teacher : BaseEntity
    {
        public Teacher()
        {
            Classrooms = new List<Classroom>();
        }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(2)]
        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }


        public List<Classroom> Classrooms { get; set; }
    }
}
