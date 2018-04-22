using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pantokrator.Repository.Contracts {
    public interface IReadonlyRepository {
        #region Sync Methods
        int ExecNonQueryStoredProc (string sqlCommand, dynamic parameters);
        int ExecNonQuerySql (string sqlCommand, dynamic parameters);
        IEnumerable<TRes> ExecStoredProcedure<TRes> (string procedureName, dynamic parameters);
        #endregion

        #region Async Methods
        Task<IEnumerable<TRes>> ExecStoredProcedureAsync<TRes> (string procedureName, dynamic parameters);
        Task<int> ExecNonQuerySqlAsync (string sqlCommand, dynamic parameters);
        Task<int> ExecNonQueryStoredProcAsync (string sqlCommand, dynamic parameters);
        #endregion
    }
}