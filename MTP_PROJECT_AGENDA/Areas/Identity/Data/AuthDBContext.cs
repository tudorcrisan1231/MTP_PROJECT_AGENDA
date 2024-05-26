using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTP_PROJECT_AGENDA.Areas.Identity.Data;
using MTP_PROJECT_AGENDA.Models;

namespace MTP_PROJECT_AGENDA.Data;

public class AuthDBContext : IdentityDbContext<ApplicationUser>
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Agenda> agenda { get; set; }
}
