using Microsoft.Extensions.DependencyInjection;
using TempusHiring.BusinessLogic.Interfaces;
using TempusHiring.BusinessLogic.Services;

namespace TempusHiring.Presentation.Extensions
{
    public static class BusinessLogicLayerServicesExtensions
    {
        public static void AddBusinessLogicLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
