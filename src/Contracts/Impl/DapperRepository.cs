using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Pantokrator.Repository.Extensions;

namespace Pantokrator.Repository.Contracts.Impl
{
    public class DapperRepository : IDapperRepository
    {
        #region Fields
        private readonly IDbConnection _connection;
        #endregion

        #region ctor
        protected DapperRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        #endregion

        #region Sync Methods

        public int ExecNonQueryStoredProc(string sqlCommand, dynamic parameters) =>
            DapperExtensions.ExecNonQueryStoredProc(_connection, sqlCommand, parameters);

        public int ExecNonQuerySql(string sqlCommand, dynamic parameters) =>
            DapperExtensions.ExecNonQuerySql(_connection, sqlCommand, parameters);

        public IEnumerable<TRes> ExecStoredProcedure<TRes>(string procedureName, dynamic parameters) =>
            DapperExtensions.ExecStoredProcedure<TRes>(_connection, procedureName, parameters);

        #endregion

        #region Async Methods

        public async Task<IEnumerable<TRes>> ExecStoredProcedureAsync<TRes>(string procedureName, dynamic parameters) =>
            await DapperExtensions.ExecStoredProcedureAsync<TRes>(_connection, procedureName, parameters);

        public async Task<int> ExecNonQueryStoredProcAsync(string sqlCommand, dynamic parameters) =>
            await DapperExtensions.ExecNonQueryStoredProcAsync(_connection, sqlCommand, parameters);

        public async Task<int> ExecNonQuerySqlAsync(string sqlCommand, dynamic parameters) =>
            await DapperExtensions.ExecNonQuerySqlAsync(_connection, sqlCommand, parameters);
        
        #endregion
    }
}