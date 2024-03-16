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
    public class HumanResourcesSeedData : IEntityTypeConfiguration<HumanResources>
    {
        public void Configure(EntityTypeBuilder<HumanResources> builder)
        {
            builder.HasData
                (
                    new HumanResources
                    {
                        Id = 1,
                        FirstName = "İnsan Kaynakları",
                        LastName = "İnsan Kaynakları",
                        Email = "hrpersonal@test.com",
                        BirthDate = new DateTime(1996, 01, 01),
                        HireDate = new DateTime(2023, 05, 05),
                        AppUserID = "8b3cd4dd-84f7-4c44-8279-7124a458dfbf"
                    }
                );
        }
    }
}
