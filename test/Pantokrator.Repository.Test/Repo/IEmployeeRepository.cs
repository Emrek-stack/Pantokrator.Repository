using Pantokrator.Repository.Contracts;
using Pantokrator.Repository.Test.Context.AdventureWorks;

namespace Pantokrator.Repository.Test.Repo
{
    public interface IEmployeeRepository : IEfRepository<Employee>
    {
        
    }
}