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
    public class HumanResourcesRepository : BaseRepository<HumanResources>, IHumanResourcesRepository
    {
        public HumanResourcesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
