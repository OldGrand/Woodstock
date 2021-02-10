using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TempusHiring.Common;
using TempusHiring.DataAccess;
using TempusHiring.DataAccess.Entities;
using TempusHiring.Presentation.Extensions;

namespace TempusHiring.Presentation
{
    public class Startup
    {
        private IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WoodstockDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddRouting(option =>
            {
                //option.LowercaseQueryStrings = true;
                //option.LowercaseUrls = true;
            });

            services.AddAutoMapper(typeof(ProfilePL));
            services.AddBusinessLogicLayerServices();

            services.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 8;

                config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<WoodstockDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/LoginRegister";
                config.AccessDeniedPath = "/Home/AccessDenied";
                config.LogoutPath = "/Account/Logout";
            });

            services.AddAuthentication()
                .AddFacebook(config =>
                {
                    config.AppId = _configuration["Authentication:Facebook:AppId"];
                    config.AppSecret = _configuration["Authentication:Facebook:AppSecret"];
                })
                .AddVkontakte(config =>
                {
                    config.ClientId = _configuration["Authentication:VK:AppId"];
                    config.ClientSecret = _configuration["Authentication:VK:AppSecret"];
                })
                .AddGoogle(config =>
                {
                    config.ClientId = _configuration["Authentication:Google:AppId"];
                    config.ClientSecret = _configuration["Authentication:Google:AppSecret"];
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(ClaimRoles.Admin, builder =>
                {
                    builder.RequireRole(ClaimRoles.Admin);
                });

                config.AddPolicy(ClaimRoles.Manager, builder =>
                {
                    builder.RequireRole(ClaimRoles.Manager);
                });

                config.AddPolicy(ClaimRoles.User, builder =>
                {
                    builder.RequireRole(ClaimRoles.User);
                });
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}