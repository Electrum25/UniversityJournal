using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL; // Для Npgsql
using UniversityJournal.Storage.EfCore; // Для UniversityJournalDbContext
using UniversityJournal.Core.Repositories; // Для интерфейсов репозиториев
using UniversityJournal.Storage.EfCore.Repositories; // Для реализаций репозиториев
using UniversityJournal.Core.UseCases;
using UniversityJournal.EfCore; // Для use cases

namespace UniversityJournal.Forms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddDbContext<UniversityJournalDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=university_journal;Username=postgres;Password=123;")); 

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();

            services.AddScoped<CreateUserUseCase>();
            services.AddScoped<AuthenticateUserUseCase>();
            services.AddScoped<CreateTeacherUseCase>();
            services.AddScoped<CreateStudentUseCase>();
            services.AddScoped<CreateGradeUseCase>();
            services.AddScoped<GetGroupsUseCase>();
            services.AddScoped<CreateGroupUseCase>();
            services.AddScoped<CreateAttendanceUseCase>();
            services.AddScoped<CreateSubjectUseCase>();
            services.AddScoped<GetSubjectsByTeacherUseCase>();
            services.AddScoped<CreateStudentSubjectUseCase>();
            services.AddScoped<GetStudentDataUseCase>();
            services.AddScoped<UpdateStudentSubjectUseCase>();
            services.AddScoped<DeleteUserUseCase>();
            services.AddScoped<DeleteGroupUseCase>();
            services.AddScoped<DeleteGroupWithStudentsUseCase>();
            services.AddScoped<DeleteSubjectUseCase>();
            services.AddScoped<UnlinkStudentSubjectUseCase>();
            services.AddScoped<UpdateTeacherUseCase>();
            services.AddScoped<UpdateStudentUseCase>();
            services.AddScoped<UpdateGroupUseCase>();
            services.AddScoped<UpdateSubjectUseCase>();
            services.AddScoped<ExportDatabaseToExcelUseCase>();
            services.AddScoped<GetTeacherResultsUseCase>();

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UniversityJournalDbContext>();
                try
                {
                    if (!context.Database.CanConnect())
                    {
                        MessageBox.Show("Не удалось подключиться к базе данных. Проверьте настройки PostgreSQL.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Application.Run(new Form1(serviceProvider)); 
        }
    }
}