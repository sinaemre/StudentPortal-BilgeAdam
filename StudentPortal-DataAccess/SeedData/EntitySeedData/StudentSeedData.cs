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
    public class StudentSeedData : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
                (
                    new Student
                    {
                        Id = 1,
                        FirstName = "Öğrenci - 1",
                        LastName = "Öğrenci - 1",
                        BirthDate = new DateTime(1996, 01, 01),
                        Email = "student@test.com",
                        ClassroomId = 1
                    },
                    new Student
                    {
                        Id = 2,
                        FirstName = "Öğrenci - 2",
                        LastName = "Öğrenci - 2",
                        BirthDate = new DateTime(1996, 02, 02),
                        Email = "student2@test.com",
                        ClassroomId = 1
                    }
                ) ;
        }
    }
}
