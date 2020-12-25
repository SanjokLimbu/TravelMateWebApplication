using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelMate.ModelFolder.IdentityModel;

namespace TravelMate.ModelFolder.ContextFolder
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<CoronaListCountryContext> CoronaListCountries { get; set; }
        public DbSet<GlobalCasesContext> GlobalContexts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CoronaListCountryContext>().HasIndex(U => U.Country).IsUnique();
        }
    }
}
