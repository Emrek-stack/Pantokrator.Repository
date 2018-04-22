using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Pantokrator.Repository.Extensions
{
    public static class DapperExtensions
    {




        #region Sync Methods
        private static void OpenConnection(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }
        private static void CloseConnection(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        private static TRes ConnectionAction<TRes>(this IDbConnection connection, Func<TRes> action)
        {
            connection.OpenConnection();
            var res = action();
            connection.CloseConnection();
            return res;
        }
        private static int ExecNonQueryInside(this IDbConnection connection, string sqlCommand, dynamic parameters, CommandType commandType)
        {
            return connection.Execute(sqlCommand, (object)parameters, null, null, commandType);
        }
        private static IEnumerable<TRes> ExecuteInside<TRes>(this IDbConnection connection, string command, dynamic parameters, CommandType commandType)
        {
            return connection.ConnectionAction(() => connection.Query<TRes>(command, (object)parameters, null, false, null, commandType));
        }

        public static int ExecNonQueryStoredProc(this IDbConnection connection, string sqlCommand, dynamic parameters)
        {
            return connection.ExecNonQueryInside(sqlCommand, (object)parameters, CommandType.StoredProcedure);
        }
        public static int ExecNonQuerySql(this IDbConnection connection, string sqlCommand, dynamic parameters)
        {
            return connection.ExecNonQueryInside(sqlCommand, (object)parameters, CommandType.Text);
        }
        public static IEnumerable<TRes> ExecStoredProcedure<TRes>(this IDbConnection connection, string procedureName, dynamic parameters)
        {
            return connection.ExecuteInside<TRes>(procedureName, (object)parameters, CommandType.StoredProcedure);
        }
        #endregion

        #region Async Methods
        private static void CloseConnectionAsync(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        private static async Task<TRes> ConnectionActionAsync<TRes>(this IDbConnection connection, Func<Task<TRes>> action)
        {
            connection.OpenConnection();
            var res = action();
            connection.CloseConnection();
            return await res;
        }
        private static async Task<int> ExecNonQueryInsideAsync(this IDbConnection connection, string sqlCommand, dynamic parameters, CommandType commandType)
        {
            return await connection.ExecuteAsync(sqlCommand, (object)parameters, null, null, commandType);
        }
        private static async Task<IEnumerable<TRes>> ExecuteInsideAsync<TRes>(this IDbConnection connection, string command, dynamic parameters, CommandType commandType)
        {
            return await connection.ConnectionAction(() => connection.QueryAsync<TRes>(command, (object)parameters, null, null, commandType));
        }


        public static async Task<int> ExecNonQueryStoredProcAsync(this IDbConnection connection, string sqlCommand, dynamic parameters)
        {
            return await connection.ExecNonQueryInsideAsync(sqlCommand, (object)parameters, CommandType.StoredProcedure);
        }
        public static async Task<int> ExecNonQuerySqlAsync(this IDbConnection connection, string sqlCommand, dynamic parameters)
        {
            return await connection.ExecNonQueryInsideAsync(sqlCommand, (object)parameters, CommandType.Text);
        }
        public static async Task<IEnumerable<TRes>> ExecStoredProcedureAsync<TRes>(this IDbConnection connection, string procedureName, dynamic parameters)
        {
            return await connection.ExecuteInsideAsync<TRes>(procedureName, (object)parameters, CommandType.StoredProcedure);
        }

        public static async Task<SqlMapper.GridReader> MultipleQueryAsync(this IDbConnection connection,
            string procedureName, dynamic parameters)
        {
            return await connection.ConnectionActionAsync(() =>
                connection.QueryMultipleAsync(procedureName, (object)parameters, null, null,
                    CommandType.StoredProcedure));
        }
        #endregion




    }
}