using Nikita_Yarancev_kt_31_23.Interfaces.DisciplinesInterfaces;
using Nikita_Yarancev_kt_31_23.Interfaces.GradesInterfaces;
using Nikita_Yarancev_kt_31_23.Interfaces.GroupsInterfaces;
using Nikita_Yarancev_kt_31_23.Interfaces.SpecialtiesInterfaces;
using Nikita_Yarancev_kt_31_23.Interfaces.StudentsInterfaces;

namespace Nikita_Yarancev_kt_31_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IGradeService, GradeService>();

            return services;
        }
    }
}
