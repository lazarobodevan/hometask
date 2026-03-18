using hometask.Data;
using hometask.Data.Repositories;
using hometask.Data.Seeds;
using hometask.Services;
using hometask.Services.Impl;
using hometask.UseCases;
using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace hometask.DI {
    public static class DependencyInjection {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DatabaseContext>(opt => {
                opt.UseNpgsql(connectionString);
            });

            services.AddScoped<InitialSeeder>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.Scan(scan => scan
                .FromAssemblies(typeof(Program).Assembly) 
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>))) 
                .AsImplementedInterfaces() 
                .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services) {
            services.Scan(scan => scan
            .FromAssemblies(typeof(Program).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IUseCase)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IRotationService, RotationService>();

            return services;
        }

    }
}
