
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Infraestructure.Data;
using AuthenticationApi.Infraestructure.Repositories;
using ECommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationApi.Infraestructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add database connectivity
            // JWT Add Authentication Scheme
            SharedServiceContainer.AddSharedServices<AutheticationDbContext>(services, config, config["MySerilog:FileName"]);

            services.AddScoped<IUser, UserRepository>();

            return services;
        }


        public static IApplicationBuilder UserInfrastructurePolicy(this IApplicationBuilder app)
        {
            // Register middleware such as:
            // Global Exception -> handle external errors
            // ListenToApiGateway Only -> block all outsiders calls
            SharedServiceContainer.UseSharedPolicies(app);

            return app;
        }

    }
}
