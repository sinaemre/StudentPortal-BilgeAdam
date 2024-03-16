using Microsoft.AspNetCore.Http;
using StudentPortal_Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.Entities.Concrete
{
    public class Student : BaseEntity
    {
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

        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }

        public double? Average 
        { 
            get 
            {
                return (0.25 * Exam1) + (0.25 * Exam2) + (0.5 * ProjectExam);
            }
        }

        public string? ProjectPath { get; set; }

        [NotMapped]
        public IFormFile? Project { get; set; }


        public int? ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

    }
}
