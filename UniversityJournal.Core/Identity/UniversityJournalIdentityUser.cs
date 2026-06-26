using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Identity;

public class UniversityJournalIdentityUser : IdentityUser<Guid>
{
    public virtual User? User { get; set; }
}