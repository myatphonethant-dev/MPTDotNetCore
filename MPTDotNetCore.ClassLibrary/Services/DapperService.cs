using Dapper;
using MPTDotNetCore.Shared.Models;
using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.Shared.Services;

public class DapperService<TModel> where TModel : class
{
    #region Constructor

    private string _connection;

    public DapperService(string connection)
    {
        _connection = connection;
    }

    #endregion

    #region Get List Query

    public IEnumerable<TModel> Query(string query, object param = null!)
    {
        using IDbConnection db = new SqlConnection(_connection);
        return db.Query<TModel>(query, param).ToList();
    }

    #endregion

    #region GetById Query

    public TModel QuerySingle(string query, object param = null!)
    {
        using IDbConnection db = new SqlConnection(_connection);
        return db.Query<TModel>(query, param).FirstOrDefault()!;
    }

    #endregion

    #region Execute Query

    public void Execute(string query, string operation, object param = null!)
    {
        using IDbConnection db = new SqlConnection(_connection);
        int result = db.Execute(query, param);

        Console.WriteLine(result > 0 ? $"{operation} Successful." : $"{operation} Failed. No rows affected.");
    }

    #endregion

    #region Generate Data

    public void DataList(IEnumerable<TModel> items)
    {
        if (items == null) return;

        foreach (var item in items)
        {
            DataList(item);
            Console.WriteLine("===============================");
        }
    }

    public void DataList(TModel item)
    {
        if (item == null) return;

        if (item is BlogModel blog)
        {
            Console.WriteLine($"Id: {blog.BlogId}");
            Console.WriteLine($"Title: {blog.BlogTitle}");
            Console.WriteLine($"Author: {blog.BlogAuthor}");
            Console.WriteLine($"Content: {blog.BlogContent}");
        }
    }

    #endregion
}