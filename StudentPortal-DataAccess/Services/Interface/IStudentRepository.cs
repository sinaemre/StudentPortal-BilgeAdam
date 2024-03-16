using StudentPortal_Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.Services.Interface
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
    }
}
