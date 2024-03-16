using StudentPortal_Core.Entities.Abstract;

namespace StudentPortal_WEB.Models.ViewModels
{
    public class GetClassroomVM
    {
        public int Id { get; set; }
        public string? ClassroomName { get; set; }
        public string? ClassroomDescription { get; set; }
        public string? TeacherName { get; set; }
        public int? ClassroomSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
