using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPortal_Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.SeedData.EntitySeedData
{
    public class ClassroomSeedData : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData
                (
                    new Classroom
                    {
                        Id = 1,
                        ClassroomName = "YZL-8445",
                        ClassroomDescription = "320 Saat .NET Full Stack Yazılım Uzmanlığı Eğitimi",
                        TeacherId = 1
                    }
                );
        }
    }
}
