using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pantokrator.Repository.Extensions;
using Pantokrator.Repository.Test;

namespace Boyner.Consoles.Test
{
    public static class AppStartup
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }
        public static void Run()
        {
            Startup();
            ConfigureServices();
        }

        private static void Startup()
        {
            if (Configuration != null) return;
            // get appsettings
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        /// <summary>
        /// Config .Net Core built in Dependency Injection
        /// </summary>
        private static void ConfigureServices()
        {
            if (ServiceProvider != null) return;

            var services = new ServiceCollection();
      
            //Register Ecomm Connection
            IDbConnection connection = new SqlConnection(Configuration.GetConnectionString("Modules:EcommDb"));
            services.AddScoped(p => connection);

            // Add functionality to inject IOptions<T>
            services.AddOptions();

           // // Add our Config object so it can be injected
           //services.Configure<AppSettings>(Configuration.GetSection("Settings"));

            services.AddLogging(loggingBuilder =>
                loggingBuilder                    
                    .AddDebug()
                    .AddConsole());


            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton<IConfiguration>(Configuration);

            //Register Data Module
            services.AddRepositoryModule();

            services.AddScoped<TestIndex>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}