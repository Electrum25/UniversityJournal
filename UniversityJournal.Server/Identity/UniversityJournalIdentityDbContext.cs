using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;

namespace UniversityJournal.Identity;

public class UniversityJournalIdentityDbContext : IdentityDbContext<UniversityJournalIdentityUser, IdentityRole<Guid>, Guid>
{
    public UniversityJournalIdentityDbContext(DbContextOptions<UniversityJournalIdentityDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    public DbSet<OpenIddictEntityFrameworkCoreApplication> OpenIddictApplications { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreAuthorization> OpenIddictAuthorizations { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreScope> OpenIddictScopes { get; set; }
    public DbSet<OpenIddictEntityFrameworkCoreToken> OpenIddictTokens { get; set; }
    public static readonly Guid AdminStaticId = Guid.Parse("019da581-0291-79d3-aae2-bae4cdd9a4a9");
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UniversityJournalIdentityUser>()
            .HasOne(u => u.User)
            .WithOne()
            .HasForeignKey<User>(u => u.IdentityUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.UseOpenIddict<OpenIddictEntityFrameworkCoreApplication,
                              OpenIddictEntityFrameworkCoreAuthorization,
                              OpenIddictEntityFrameworkCoreScope,
                              OpenIddictEntityFrameworkCoreToken,
                              string>();
    }

    public static async Task SeedAsync(UniversityJournalIdentityDbContext context,
        UserManager<UniversityJournalIdentityUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        await context.Database.EnsureCreatedAsync();

        string[] roles = { "Admin", "Teacher", "Student" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }

        if (await userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new UniversityJournalIdentityUser
            {
                Id = AdminStaticId,
                UserName = "admin",
                Email = "admin@university.com",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        if (await userManager.FindByNameAsync("teacher") == null)
        {
            var teacherUser = new UniversityJournalIdentityUser
            {
                UserName = "teacher",
                Email = "teacher@university.com",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(teacherUser, "Teacher123!");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(teacherUser, "Teacher");
        }
    }
}