using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace UniversityJournal.Identity;
public class UniversityJournalIdentityDbContext : IdentityDbContext<UniversityJournalIdentityUser>
{
    public UniversityJournalIdentityDbContext(DbContextOptions<UniversityJournalIdentityDbContext> options) : base(options) 
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated(); 
    }
}