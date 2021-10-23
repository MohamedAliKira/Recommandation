using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Services
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthService, HttpAuthService>();
                         //  .AddScoped<IPlansService, HttpPlansService>();
        }
    }
}
