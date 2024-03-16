using StudentPortal_Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.Entities.Concrete
{
    public class Classroom : BaseEntity
    {
        public Classroom()
        {
            Students = new List<Student>(); 
        } 

        public string? ClassroomName { get; set; }
        public string? ClassroomDescription { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public List<Student> Students { get; set; }
    }
}
