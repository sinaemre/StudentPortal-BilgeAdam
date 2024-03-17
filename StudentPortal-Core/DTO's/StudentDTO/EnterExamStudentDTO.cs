using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.StudentDTO
{
    public class EnterExamStudentDTO
    {
        public int Id { get; set; }
        public string AppUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int ClassroomId { get; set; }

        [Display(Name = "Sınav - 1")]
        [Range(0, 100, ErrorMessage = "0 ile 100 arasında giriş yapabilirisiniz!")]
        public double? Exam1 { get; set; }
       
        [Display(Name = "Sınav - 2")]
        [Range(0, 100, ErrorMessage = "0 ile 100 arasında giriş yapabilirisiniz!")]
        public double? Exam2 { get; set; }


        [Display(Name = "Proje Notu")]
        [Range(0, 100, ErrorMessage = "0 ile 100 arasında giriş yapabilirisiniz!")]
        public double? ProjectExam { get; set; }

        public string? ProjectPath { get; set; }
        public IFormFile? Project { get; set; }


    }
}
