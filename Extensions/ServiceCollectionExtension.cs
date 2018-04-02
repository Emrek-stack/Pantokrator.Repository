using Frost.Data.Sql.Contracts;
using Frost.Data.Sql.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Frost.Data.Sql.Extensions {
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