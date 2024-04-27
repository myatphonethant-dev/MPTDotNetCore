using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MPTDotNetCore.Shared.Services
{
    public class DapperService<TModel> where TModel : class
    {
        #region Constructor

        private string _connection;

        public DapperService(string connection)
        {
            _connection = connection;
        }

        #endregion

        public IEnumerable<TModel> Query(string sql, object param = null!)
        {
            using IDbConnection db = new SqlConnection(_connection);
            return db.Query<TModel>(sql, param).ToList();
        }

        public TModel QuerySingle(string sql, object param = null!)
        {
            using IDbConnection db = new SqlConnection(_connection);
            return db.Query<TModel>(sql, param).FirstOrDefault()!;
        }

        public void Execute(string sql, object param = null!)
        {
            using IDbConnection db = new SqlConnection(_connection);
            int result = db.Execute(sql, param);

            string message = result > 0 ? "Operation Successful." : "Operation Failed.";
            Console.WriteLine(message);
            Console.WriteLine("===============================");
        }
    }
}
