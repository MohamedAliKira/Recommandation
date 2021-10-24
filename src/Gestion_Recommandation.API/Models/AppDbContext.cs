using Gestion_Recommandation.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Recommandation.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Recommandations> Recommandations { get; set; }
        public DbSet<Trace_Recommandations> Trace_Recommandations { get; set; }
    }
}
