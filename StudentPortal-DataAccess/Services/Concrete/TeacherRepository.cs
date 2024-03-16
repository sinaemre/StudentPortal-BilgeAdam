using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_DataAccess.Context;
using StudentPortal_DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.Services.Concrete
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> ClassroomNames(int teacherId)
        {
            var classrooms = await _context.Classrooms.Where(x => x.Status != StudentPortal_Core.Entities.Abstract.Status.Passive && x.TeacherId == teacherId).ToListAsync();
            
            var namesList = new List<string>();
            foreach (var classroom in classrooms)
            {
                namesList.Add(classroom.ClassroomName);
            }

            var classroomNames = string.Join(',', namesList);
            return classroomNames;
        }

        public async Task<bool> HasClassroom(int teacherId) => await _context.Classrooms.AnyAsync(x => x.Status != StudentPortal_Core.Entities.Abstract.Status.Passive && x.TeacherId == teacherId);
    }
}
