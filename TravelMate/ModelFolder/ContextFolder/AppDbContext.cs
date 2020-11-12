using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelMate.ModelFolder.IdentityModel;

namespace TravelMate.ModelFolder.ContextFolder
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }
    }
}
