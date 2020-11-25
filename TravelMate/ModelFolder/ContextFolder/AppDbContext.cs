using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelMate.ModelFolder.GlobalCoronaModel;
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
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<GlobalCasesContext>().ToTable("GlobalContexts").HasKey(GlobalContexts => GlobalContexts.Id);
            modelbuilder.Entity<CoronaListCountryContext>().ToTable("CoronaListCountries").HasKey(CoronaListCountryContext => CoronaListCountryContext.Id);
        }
    }
}
