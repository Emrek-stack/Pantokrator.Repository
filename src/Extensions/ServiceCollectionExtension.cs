using Microsoft.Extensions.DependencyInjection;
using Pantokrator.Repository.Contracts;
using Pantokrator.Repository.Contracts.Impl;

namespace Pantokrator.Repository.Extensions {
    public static class ServiceCollectionExtension {
        public static IServiceCollection AddRepositoryModule (this IServiceCollection services) {
            services                
                .AddScoped<IReadonlyRepository, ReadOnlyRepository> ()
                .AddScoped (typeof (IEfReadRepository<>), typeof (ReadRepository<,>))
                .AddScoped (typeof (IEfWriteRepository<>), typeof (WriteRepository<,>));

            return services;
        }
    }
}