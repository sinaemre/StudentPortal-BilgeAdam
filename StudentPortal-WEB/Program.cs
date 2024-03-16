using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentPortal_Core.DTO_s.AccountDTO;
using StudentPortal_Core.Entities.UserEntites.Concrete;
using StudentPortal_DataAccess.Context;
using StudentPortal_DataAccess.Context.IdentityContext;
using StudentPortal_DataAccess.DependencyResolvers.Autofac;
using StudentPortal_DataAccess.FluentValidators.AccountValidators;
using StudentPortal_DataAccess.FluentValidators.ClassroomValidators;
using StudentPortal_DataAccess.FluentValidators.HumanResourcesValidators;
using StudentPortal_DataAccess.FluentValidators.RoleValidators;
using StudentPortal_DataAccess.FluentValidators.StudentValidators;
using StudentPortal_DataAccess.FluentValidators.TeacherValidators;

namespace StudentPortal_WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateTeacherValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateTeacherValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateClassroomValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateClassroomValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateStudentValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<EditUserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateRoleValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateRoleValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateHRValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateHRValidator>();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            //Autofac kurulumu
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                        .ConfigureContainer<ContainerBuilder>(builder =>
                        {
                            builder.RegisterModule(new AutofacBusinessModule());
                        });


            var connectionString = builder.Configuration.GetConnectionString("PostgresSQLConnection");
            var connectionStringIdentity = builder.Configuration.GetConnectionString("PostgresSQLIdentityConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseNpgsql(connectionStringIdentity);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
            {
                x.SignIn.RequireConfirmedPhoneNumber = false;
                x.SignIn.RequireConfirmedAccount = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.User.RequireUniqueEmail = true;
                x.Password.RequiredLength = 1;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

          

            app.Run();
        }
    }
}
