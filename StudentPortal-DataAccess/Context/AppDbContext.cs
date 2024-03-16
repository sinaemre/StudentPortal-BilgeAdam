using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.Entities.Concrete;
using StudentPortal_DataAccess.SeedData.EntitySeedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        static AppDbContext() 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.Migrate();
            //Sizin yerinize otomatik olarak update-database komutunu çağırır.
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<HumanResources> HumanResources { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TeacherSeedData());
            modelBuilder.ApplyConfiguration(new ClassroomSeedData());
            modelBuilder.ApplyConfiguration(new StudentSeedData());
            modelBuilder.ApplyConfiguration(new HumanResourcesSeedData());
        }
    }
}
