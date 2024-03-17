using AutoMapper;
using StudentPortal_Core.DTO_s.AccountDTO;
using StudentPortal_Core.DTO_s.ClassroomDTO;
using StudentPortal_Core.DTO_s.HumanResourcesDTO;
using StudentPortal_Core.DTO_s.StudentDTO;
using StudentPortal_Core.DTO_s.TeacherDTO;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Teacher, CreateTeacherDTO>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDTO>().ReverseMap();
            
            CreateMap<Classroom, CreateClassroomDTO>().ReverseMap();
            CreateMap<Classroom, UpdateClassroomDTO>().ReverseMap();

            CreateMap<Student, CreateStudentDTO>().ReverseMap();
            CreateMap<Student, UpdateStudentDTO>().ReverseMap();
            CreateMap<Student, GetStudentDetailDTO>().ReverseMap();
            CreateMap<Student, EnterExamStudentDTO>().ReverseMap();

            CreateMap<HumanResources, CreateHRDTO>().ReverseMap();
            CreateMap<HumanResources, UpdateHRDTO>().ReverseMap();

            
        }
    }
}
