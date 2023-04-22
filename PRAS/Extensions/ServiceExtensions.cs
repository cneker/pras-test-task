using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PRAS.Contracts;
using PRAS.Contracts.Repositories;
using PRAS.Contracts.Services;
using PRAS.Mapper;
using PRAS.Persistence;
using PRAS.Persistence.Repositories;
using PRAS.Services;
using System.Globalization;
using AuthenticationService = PRAS.Services.AuthenticationService;
using IAuthenticationService = PRAS.Contracts.Services.IAuthenticationService;

namespace PRAS.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration.GetConnectionString("SqlServerConnection");

            if (conString == null)
            {
                throw new ArgumentNullException(nameof(conString));
            }

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(conString);
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(MappingProfile));

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IFileManipulationService, FileManipulationService>();
            services.AddScoped<INewsService, NewsService>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.LoginPath = "/authentication";
                });
        }

        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                opts.DefaultRequestCulture = new RequestCulture("en");
                opts.SupportedUICultures = supportedCultures;
            });
        }
    }
}
