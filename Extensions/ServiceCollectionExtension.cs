using Microsoft.Extensions.DependencyInjection;
using Pantokrator.Data.Sql.Contracts;
using Pantokrator.Data.Sql.Contracts.Impl;

namespace Pantokrator.Data.Sql.Extensions {
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