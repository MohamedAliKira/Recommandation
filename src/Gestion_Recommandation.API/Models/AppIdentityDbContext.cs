using Gestion_Recommandation.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Recommandation.API.Models
{
    public class AppIdentityDbContext : IdentityDbContext<UserIdentity>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {

        }

        public DbSet<Service> Service { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Service>().HasData(
                    new Service { Id = 1, Libelle = "DRH", Tuttelle = 1 },
                    new Service { Id = 2, Libelle = "SDP", Tuttelle = 1 },
                    new Service { Id = 3, Libelle = "SDC", Tuttelle = 1 },
                    new Service { Id = 4, Libelle = "SDRS", Tuttelle = 1 },                    
                    new Service { Id = 5, Libelle = "SCPN", Tuttelle = 1 },
                    new Service { Id = 6, Libelle = "BIC", Tuttelle = 1 },
                    new Service { Id = 7, Libelle = "BI", Tuttelle = 1 },
                    new Service { Id = 8, Libelle = "BAG", Tuttelle = 1 },
                    new Service { Id = 9, Libelle = "BFA", Tuttelle = 1 },
                    new Service { Id = 10, Libelle = "BPF", Tuttelle = 1 },
                    new Service { Id = 11, Libelle = "BCAA", Tuttelle = 1 },

                    new Service { Id = 12, Libelle = "BESM", Tuttelle = 2 },
                    new Service { Id = 13, Libelle = "BGC", Tuttelle = 2 },
                    new Service { Id = 14, Libelle = "BDA", Tuttelle = 2 },
                    new Service { Id = 15, Libelle = "BPM", Tuttelle = 2 },

                    new Service { Id = 16, Libelle = "BAT", Tuttelle = 3 },
                    new Service { Id = 17, Libelle = "BCM", Tuttelle = 3 },
                    new Service { Id = 18, Libelle = "BRP", Tuttelle = 3 },
                    new Service { Id = 19, Libelle = "BAJ", Tuttelle = 3 },

                    new Service { Id = 20, Libelle = "BS", Tuttelle = 4 },
                    new Service { Id = 21, Libelle = "BRAA", Tuttelle = 4 },
                    new Service { Id = 22, Libelle = "BSF", Tuttelle = 4 },
                    new Service { Id = 23, Libelle = "BEP", Tuttelle = 4 },
                    new Service { Id = 24, Libelle = "BPSY", Tuttelle = 4 }

            );
        }
    }
}
