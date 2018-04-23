using System;
using System.Linq;
using Pantokrator.Repository.Test.Context.AdventureWorks;
using Pantokrator.Repository.Test.Repo;

namespace Pantokrator.Repository.Test
{
    public class TestIndex
    {
        //  private readonly ILogger<TestIndex> _logger;
        private readonly IEmployeeRepository _empRepository;

        public TestIndex(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }

        public void Run()
        {
            Test();
        }

        private void Test()
        {
            var data = _empRepository.GetAll().ToList();

            foreach (Employee employee in data)
            {
                Console.WriteLine(employee.JobTitle);
            }
        }
    }
}