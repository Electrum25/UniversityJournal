using EasyCaching.Core;
using EasyCaching.InMemory;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.Storage.Repositories;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.UseCases.ScheduleUseCase;
using UniversityJournal.EfCore;
using UniversityJournal.EfCore.Repository;
using UniversityJournal.Identity;
using UniversityJournal.Server.Validators;
using UniversityJournal.Storage.EfCore.Repositories;

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

    builder.Services.AddDbContext<UniversityJournalDbContext>(o =>
        o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContext<UniversityJournalIdentityDbContext>(o =>
        o.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

    builder.Services.AddIdentity<UniversityJournalIdentityUser, IdentityRole<Guid>>(options => {
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<UniversityJournalIdentityDbContext>()
    .AddDefaultTokenProviders();

    builder.Services.AddMemoryCache();
    builder.Services.AddEasyCaching(options =>
    {
        options.UseInMemory(config =>
        {
            config.MaxRdSecond = 0; 
        }, "default");
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/connect/login";
        options.Cookie.Name = ".AspNetCore.Identity.Application";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });
    
    builder.Services.AddOpenIddict()
        .AddCore(options =>
        {
            options.UseEntityFrameworkCore()
                   .UseDbContext<UniversityJournalIdentityDbContext>();
        })
        .AddServer(options =>
        {
            options.SetAuthorizationEndpointUris("/connect/authorize")
                   .SetTokenEndpointUris("/connect/token")
                   .SetUserInfoEndpointUris("/connect/userinfo");

            options.AllowAuthorizationCodeFlow()
                   .AllowRefreshTokenFlow()
                   .AllowPasswordFlow();

            options.IgnoreScopePermissions();
            options.IgnoreEndpointPermissions();


            options.RegisterScopes(
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Roles,
                "api",
                "offline_access"
            );

            options.AddDevelopmentEncryptionCertificate()
                   .AddDevelopmentSigningCertificate();

            options.UseAspNetCore()
       .EnableAuthorizationEndpointPassthrough()
       .EnableTokenEndpointPassthrough()
       .EnableUserInfoEndpointPassthrough()
       .EnableStatusCodePagesIntegration();
        })
        .AddValidation(options =>
        {
            options.UseLocalServer();
            options.UseAspNetCore();
        });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "API";
        options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme; 
    })
    .AddPolicyScheme("API", "API Bearer or Cookie", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            var authHeader = context.Request.Headers.Authorization.ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                return OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            return IdentityConstants.ApplicationScheme;
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    });

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    builder.Services.AddScoped<IGradeRepository, GradeRepository>();
    builder.Services.AddScoped<IGroupRepository, GroupRepository>();
    builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
    builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
    builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
    builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

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
    builder.Services.AddScoped<GetScheduleByTeacherUseCase>();
    builder.Services.AddScoped<GetAllUsersUseCase>();
    builder.Services.AddScoped<GetUserByIdUseCase>();
    builder.Services.AddScoped<GetAllUsersUseCase>();
    builder.Services.AddScoped<GetUserByIdUseCase>();
    builder.Services.AddScoped<GetStudentProfileUseCase>();
    builder.Services.AddScoped<GetTeacherProfileUseCase>();
    builder.Services.AddScoped<GetUserByIdentityUseCase>();

    builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentRequestValidator>();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "University Journal API",
            Version = "v1"
        });

        options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
            Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
            {
                AuthorizationCode = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("https://localhost:7070/connect/authorize"),
                    TokenUrl = new Uri("https://localhost:7070/connect/token"),
                    Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID Connect" },
                    { "profile", "User profile" },
                    { "email", "Email address" },
                    { "roles", "User roles" },
                    { "api", "API access" },
                    { "offline_access", "Refresh token" }
                }
                }
            },
            Description = "OpenIddict Authorization Code Flow with PKCE"
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { "openid", "profile", "email", "roles", "api", "offline_access" }
        }
    });
    });

    builder.Services.AddControllers().AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

    builder.Services.AddCors(options => {
        options.AddPolicy("AllowAll", b =>
            b.WithOrigins("http://localhost:3000", "http://localhost:8080", "https://localhost:7070")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials());
    });

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UniversityJournalIdentityUser>>();
        var identityContext = scope.ServiceProvider.GetRequiredService<UniversityJournalIdentityDbContext>();
        var appManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        await UniversityJournalIdentityDbContext.SeedAsync(identityContext, userManager, roleManager);

        string[] roles = { "Admin", "Teacher", "Student" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }

        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var client = await applicationManager.FindByClientIdAsync("university_journal_mobile");

        if (client == null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "university_journal_mobile",
                ClientSecret = "secret",
                DisplayName = "University Journal Mobile App",
                RedirectUris =
        {
            new Uri("https://localhost:7070/signin-oidc"),
            new Uri("http://localhost:5000/callback"),
            new Uri("https://oauth.pstmn.io/v1/callback"),
            new Uri("https://localhost:7070/swagger/oauth2-redirect.html"),
            new Uri("https://localhost:7070/callback"),
            new Uri("https://localhost:7070/"),
            new Uri("com.universityjournal:/oauth2redirect"),
            new Uri("http://localhost:52096"),
        },
                Permissions =
        {
            OpenIddictConstants.Permissions.Endpoints.Authorization,
            OpenIddictConstants.Permissions.Endpoints.Token,
            OpenIddictConstants.Permissions.Endpoints.EndSession,
            "ept:userinfo",
            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
            OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
            OpenIddictConstants.Permissions.GrantTypes.Password,
            OpenIddictConstants.Permissions.ResponseTypes.Code,
            OpenIddictConstants.Permissions.Prefixes.Scope + "email",
            OpenIddictConstants.Permissions.Prefixes.Scope + "profile",
            OpenIddictConstants.Permissions.Prefixes.Scope + "roles",
            OpenIddictConstants.Permissions.Prefixes.Scope + "api",
            OpenIddictConstants.Permissions.Prefixes.Scope + "offline_access"
        }
            });
        }
        else
        {
            var descriptor = new OpenIddictApplicationDescriptor();
            await applicationManager.PopulateAsync(descriptor, client);

            descriptor.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Password);

            await applicationManager.UpdateAsync(client, descriptor);
        }


        var db = scope.ServiceProvider.GetRequiredService<UniversityJournalDbContext>();
        await db.Database.EnsureCreatedAsync();
        var identityAdmin = await userManager.FindByNameAsync("admin");

        var businessAdmin = await db.Users.FirstOrDefaultAsync(u => u.Login == "admin");

        if (identityAdmin != null && businessAdmin != null)
        {
            businessAdmin.IdentityUserId = identityAdmin.Id;
            await db.SaveChangesAsync();
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "University Journal API v1");
            options.OAuthClientId("university_journal_mobile");
            options.OAuthClientSecret("secret");
            options.OAuthScopes("openid", "profile", "email", "roles", "api", "offline_access");
            options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
    {
        { "usePkceWithAuthorizationCodeGrant", "false" }
    });
           
        });
    }

    app.UseRouting();
    app.UseCors("AllowAll");
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