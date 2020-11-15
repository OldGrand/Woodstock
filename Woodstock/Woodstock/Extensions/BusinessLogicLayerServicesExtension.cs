using Microsoft.Extensions.DependencyInjection;
using Woodstock.BLL.Interfaces;
using Woodstock.BLL.Services;

namespace Woodstock.PL.Extensions
{
    public static class BusinessLogicLayerServicesExtension
    {
        public static void AddBusinessLogicLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
