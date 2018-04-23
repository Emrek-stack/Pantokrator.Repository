using System;
using Microsoft.Extensions.DependencyInjection;
using Pantokrator.Repository.Contracts;
using Pantokrator.Repository.Test.Context.AdventureWorks;

namespace Pantokrator.Repository.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AppStartup.Run();
            Console.WriteLine("Press any key to start...");

            

            var testIndex = AppStartup.ServiceProvider.GetService<TestIndex>();
            testIndex.Run();
       
            

            Console.ReadLine();
        }
    }
}
