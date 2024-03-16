using Autofac;
using AutoMapper;
using StudentPortal_DataAccess.AutoMapper;
using StudentPortal_DataAccess.Services.Concrete;
using StudentPortal_DataAccess.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal_DataAccess.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Services
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ClassroomRepository>().As<IClassroomRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HumanResourcesRepository>().As<IHumanResourcesRepository>().InstancePerLifetimeScope();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance<IMapper>(mapper);

        }
    }
}
