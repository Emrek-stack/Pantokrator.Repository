using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Pantokrator.Repository.Extensions;

namespace Pantokrator.Repository.Contracts.Impl
{
    public abstract class ReadOnlyRepository : IReadonlyRepository
    {
        #region Fields
        private readonly IDbConnection _connection;
        #endregion

        #region ctor
        protected ReadOnlyRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        #endregion

        #region Sync Methods
        public int ExecNonQueryStoredProc(string sqlCommand, dynamic parameters)
        {
            return DapperExtensions.ExecNonQueryStoredProc(_connection, sqlCommand, parameters);
        }
        public int ExecNonQuerySql(string sqlCommand, dynamic parameters)
        {
            return DapperExtensions.ExecNonQuerySql(_connection, sqlCommand, parameters);
        }
        public IEnumerable<TRes> ExecStoredProcedure<TRes>(string procedureName, dynamic parameters)
        {
            return DapperExtensions.ExecStoredProcedure<TRes>(_connection, procedureName, parameters);
        }
        #endregion

        #region Async Methods
        public async Task<IEnumerable<TRes>> ExecStoredProcedureAsync<TRes>(string procedureName, dynamic parameters)
        {
            return await DapperExtensions.ExecStoredProcedureAsync<TRes>(_connection, procedureName, parameters);
        }
        public async Task<int> ExecNonQueryStoredProcAsync(string sqlCommand, dynamic parameters)
        {
            return await DapperExtensions.ExecNonQueryStoredProcAsync(_connection, sqlCommand, parameters);
        }
        public async Task<int> ExecNonQuerySqlAsync(string sqlCommand, dynamic parameters)
        {
            return await DapperExtensions.ExecNonQuerySqlAsync(_connection, sqlCommand, parameters);
        }


        #endregion        
    }
}