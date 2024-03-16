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
    public class IdentityUserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
                (
                    new IdentityUserRole<string>
                    {
                        UserId = "92881b6d-cb5d-4809-b964-91074a5184d1",
                        RoleId = "b609c887-e794-4762-be5a-6c95232812a4"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "9d14c127-c5ec-4372-8ba9-26d58ebcdbe1",
                        RoleId = "74f37192-b74b-4330-b875-372e82c04002"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "2735fcfe-c490-4055-ae67-18ae6eca2212",
                        RoleId = "74f37192-b74b-4330-b875-372e82c04002"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "427f1691-2f27-44bb-b9f1-d1a4782381af",
                        RoleId = "e256341e-70f6-4573-b09a-ab4205a7efc6"
                    },
                    new IdentityUserRole<string>
                    {
                        UserId = "8b3cd4dd-84f7-4c44-8279-7124a458dfbf",
                        RoleId = "dd8eadf8-f90f-41f3-9d81-096ef9e7829b"
                    }
                );
        }
    }
}
