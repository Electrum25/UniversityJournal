using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Serilog;
using Serilog.Events;
using UniversityJournal.EfCore;
using UniversityJournal.Identity;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Storage.EfCore.Repositories;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.UseCases.ScheduleUseCase;
using UniversityJournal.Core.Storage.Repositories;
using UniversityJournal.EfCore.Repository;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/journal-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // 1. Контексты данных
    builder.Services.AddDbContext<UniversityJournalDbContext>(o =>
        o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContext<UniversityJournalIdentityDbContext>(o =>
        o.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

    // 2. Identity
    builder.Services.AddIdentity<UniversityJournalIdentityUser, IdentityRole>(options => {
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<UniversityJournalIdentityDbContext>()
    .AddDefaultTokenProviders();

    // 2. Настройка схем аутентификации
    builder.Services.AddAuthentication(options => {
        // Указываем, что куки — это основная схема для всего
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options => {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);

        // 1. Исправляем путь (теперь редирект будет на твой AuthController)
        options.LoginPath = "/api/auth/login";

        // 2. Чтобы API не делал редирект, а просто отдавал 401 ошибку (очень полезно для фронтенда)
        options.Events.OnRedirectToLogin = context => {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

    // 3. Репозитории
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    builder.Services.AddScoped<IGradeRepository, GradeRepository>();
    builder.Services.AddScoped<IGroupRepository, GroupRepository>();
    builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
    builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
    builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
    builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

    // 4. UseCases
    builder.Services.AddScoped<CreateUserUseCase>();
    builder.Services.AddScoped<AuthenticateUserUseCase>();
    builder.Services.AddScoped<CreateTeacherUseCase>();
    builder.Services.AddScoped<CreateStudentUseCase>();
    builder.Services.AddScoped<CreateGradeUseCase>();
    builder.Services.AddScoped<GetGroupsUseCase>();
    builder.Services.AddScoped<CreateGroupUseCase>();
    builder.Services.AddScoped<CreateAttendanceUseCase>();
    builder.Services.AddScoped<CreateSubjectUseCase>();
    builder.Services.AddScoped<GetSubjectsByTeacherUseCase>();
    builder.Services.AddScoped<CreateStudentSubjectUseCase>();
    builder.Services.AddScoped<GetStudentDataUseCase>();
    builder.Services.AddScoped<UpdateStudentSubjectUseCase>();
    builder.Services.AddScoped<DeleteUserUseCase>();
    builder.Services.AddScoped<DeleteGroupUseCase>();
    builder.Services.AddScoped<DeleteGroupWithStudentsUseCase>();
    builder.Services.AddScoped<DeleteSubjectUseCase>();
    builder.Services.AddScoped<UnlinkStudentSubjectUseCase>();
    builder.Services.AddScoped<UpdateTeacherUseCase>();
    builder.Services.AddScoped<UpdateStudentUseCase>();
    builder.Services.AddScoped<UpdateGroupUseCase>();
    builder.Services.AddScoped<UpdateSubjectUseCase>();
    builder.Services.AddScoped<ExportDatabaseToExcelUseCase>();
    builder.Services.AddScoped<GetTeacherResultsUseCase>();
    builder.Services.AddScoped<GetScheduleByGroupUseCase>();

    // 5. Swagger - МИНИМАЛЬНО (как в CinemaReview)
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // 6. Контроллеры
    builder.Services.AddControllers().AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

    builder.Services.AddCors(options => {
        options.AddPolicy("AllowWithCredentials", b =>
            b.WithOrigins("http://localhost:3000")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials());
    });

    var app = builder.Build();

    // 7. Инициализация ролей
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = { "Admin", "Teacher", "Student" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<UniversityJournalDbContext>();
        // Эта команда применит все изменения из OnModelCreating к таблицам в PostgreSQL
        await db.Database.EnsureCreatedAsync();
        // Если миграции уже были, лучше использовать:
        // await db.Database.MigrateAsync(); 
    }

    // 8. Swagger UI - МИНИМАЛЬНО (как в CinemaReview)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();
    app.UseCors("AllowWithCredentials");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}