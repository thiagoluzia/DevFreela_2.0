using DevFreela.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // o que será injetado na class program
            services
                .AddServices()
                .AddServices2();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            // meus injetaveis
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUSerService, USerService>();

            return services;
        }

        // Outras tipos de injeção 
        private static IServiceCollection AddServices2(this IServiceCollection services)
        {
            //meus injetaveis
            //services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}
