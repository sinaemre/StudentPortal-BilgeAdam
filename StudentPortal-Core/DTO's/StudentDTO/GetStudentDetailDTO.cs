using StudentPortal_Core.Entities.Abstract;

namespace StudentPortal_Core.DTO_s.StudentDTO
{
    public class GetStudentDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? ClassroomName { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public string? ProjectName { get; set; }
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
                }
                return false;
            } 
        }

        public double? Average { get; set; }
        public string? TeacherName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
