using Pantokrator.Repository.Contracts.Impl;
using Pantokrator.Repository.Test.Context.AdventureWorks;

namespace Pantokrator.Repository.Test.Repo.Impl
{
    public class EmployeeRepository : EfRepository<Employee, AdventureWorks>, IEmployeeRepository
    {
        public EmployeeRepository(AdventureWorks context) : base(context)
        {
        }
    }
}