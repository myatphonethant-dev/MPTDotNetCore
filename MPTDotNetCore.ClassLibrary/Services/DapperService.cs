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

    public void DataList(IEnumerable<BlogModel> items)
    {
        if (items == null) return;

        foreach (var item in items) DataList(items);
    }

    public void DataList(BlogModel item)
    {
        if (item == null) return;

        Console.WriteLine($"Id: {item.BlogId}");
        Console.WriteLine($"Title: {item.BlogTitle}");
        Console.WriteLine($"Author: {item.BlogAuthor}");
        Console.WriteLine($"Content: {item.BlogContent}");
    }

    #endregion
}