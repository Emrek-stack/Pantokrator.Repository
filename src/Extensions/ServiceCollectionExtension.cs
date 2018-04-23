using Microsoft.Extensions.DependencyInjection;
using Pantokrator.Repository.Contracts;
using Pantokrator.Repository.Contracts.Impl;

namespace Pantokrator.Repository.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryModule(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IEfRepository<>), typeof(EfRepository<,>));
                //.AddScoped<IDapperRepository, DapperRepository>();


            return services;
        }
    }
}