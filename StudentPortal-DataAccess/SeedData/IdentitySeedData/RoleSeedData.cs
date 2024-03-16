using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.SeedData.IdentitySeedData
{
    public class RoleSeedData : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var admin = new IdentityRole
            {
                Id = "b609c887-e794-4762-be5a-6c95232812a4",
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var student = new IdentityRole
            {
                Id = "74f37192-b74b-4330-b875-372e82c04002",
                Name = "student",
                NormalizedName = "STUDENT"
            };

            var teacher = new IdentityRole
            {
                Id = "e256341e-70f6-4573-b09a-ab4205a7efc6",
                Name = "teacher",
                NormalizedName = "TEACHER"
            };

            var hrPersonal = new IdentityRole
            {
                Id = "dd8eadf8-f90f-41f3-9d81-096ef9e7829b",
                Name = "hrPersonal",
                NormalizedName = "HRPERSONAL"
            };

            builder.HasData(admin, student, teacher, hrPersonal);

        }
    }
}
