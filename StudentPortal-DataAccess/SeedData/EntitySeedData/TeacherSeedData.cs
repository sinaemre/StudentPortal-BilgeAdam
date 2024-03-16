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
    public class TeacherSeedData : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData
                (
                    new Teacher
                    {
                        Id = 1, 
                        FirstName = "Öğretmen - 1",
                        LastName = "Öğretmen - 1",
                        BirthDate = new DateTime(1996,01,23),
                        Email = "teacher@test.com",
                        AppUserID = "427f1691-2f27-44bb-b9f1-d1a4782381af"
                    }
                );
        }
    }
}
