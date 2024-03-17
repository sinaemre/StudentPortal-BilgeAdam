using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_Core.DTO_s.StudentDTO
{
    public class EnterExamStudentDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public double? Average { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public bool? IsSucceed
        {
            get
            {
                if (Average is not null)
                {
                    if (Average >= 70)
                    {
                        return true;
                    }
                    else { return false; }
                }
                return null;
            }
        }

    }
}
