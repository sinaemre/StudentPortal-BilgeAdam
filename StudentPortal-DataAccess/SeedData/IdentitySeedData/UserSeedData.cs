using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.SeedData.IdentitySeedData
{
    public class UserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = "92881b6d-cb5d-4809-b964-91074a5184d1",
                FirstName = "Admin",
                LastName = "Admin",
                BirthDate = new DateTime(1999,01,01),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@test.com",
                NormalizedEmail = "ADMIN@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123")
            };

            var student = new AppUser
            {
                Id = "9d14c127-c5ec-4372-8ba9-26d58ebcdbe1",
                FirstName = "Öğrenci - 1",
                LastName = "Öğrenci - 1",
                BirthDate = new DateTime(1996,01,01),
                UserName = "student",
                NormalizedUserName = "STUDENT",
                Email = "student@test.com",
                NormalizedEmail = "STUDENT@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123")
            };

            var student2 = new AppUser
            {
                Id = "2735fcfe-c490-4055-ae67-18ae6eca2212",
                FirstName = "Öğrenci - 2",
                LastName = "Öğrenci - 2",
                BirthDate = new DateTime(1996, 02, 02),
                UserName = "student2",
                NormalizedUserName = "STUDENT2",
                Email = "student2@test.com",
                NormalizedEmail = "STUDENT2@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123")
            };

            var teacher = new AppUser
            {
                Id = "427f1691-2f27-44bb-b9f1-d1a4782381af",
                FirstName = "Öğretmen - 1",
                LastName = "Öğretmen - 1",
                BirthDate = new DateTime(1996, 01, 23),
                UserName = "teacher",
                NormalizedUserName = "TEACHER",
                Email = "teacher@test.com",
                NormalizedEmail = "TEACHER@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123")
            };

            var hrPersonal = new AppUser
            {
                Id = "8b3cd4dd-84f7-4c44-8279-7124a458dfbf",
                FirstName = "İnsan Kaynakları",
                LastName = "İnsan Kaynakları",
                BirthDate = new DateTime(1996, 01, 01),
                UserName = "hrPersonal",
                NormalizedUserName = "HRPERSONAL",
                Email = "hrpersonal@test.com",
                NormalizedEmail = "HRPERSONAL@TEST.COM",
                PasswordHash = hasher.HashPassword(null, "123")
            };

            builder.HasData
                (
                    admin, student, student2, teacher, hrPersonal
                );

        }
    }
}
