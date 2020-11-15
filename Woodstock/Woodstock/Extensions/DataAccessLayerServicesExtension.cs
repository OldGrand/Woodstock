using Microsoft.Extensions.DependencyInjection;
using Woodstock.DAL.Interfaces;
using Woodstock.DAL.UnitOfWork;

namespace Woodstock.PL.Extensions
{
    public static class DataAccessLayerServicesExtension
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
