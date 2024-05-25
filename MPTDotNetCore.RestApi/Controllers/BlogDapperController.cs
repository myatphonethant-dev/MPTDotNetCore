using Dapper;
using Microsoft.AspNetCore.Mvc;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;
using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogDapperController : ControllerBase
{
    private readonly DbService _connection;

    public BlogDapperController(DbService connection)
    {
        _connection = connection;
    }

    [HttpGet]
    public IActionResult Read()
    {
        using IDbConnection db = new SqlConnection(_connection.GetConnection());

        var lst = db.Query<BlogModel>(SqlQueries.SelectQuery).ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = GetById(id);
        if (item == null) return NotFound("No Data Found.");
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(BlogModel model)
    {
        using IDbConnection db = new SqlConnection(_connection.GetConnection());

        int result = db.Execute(SqlQueries.CreateQuery, model);
        var message = result > 0 ? "Saving Successful." : "Saving Failed.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel model)
    {
        var item = GetById(id);
        if (item == null) return NotFound("No Data Found.");

        using IDbConnection db = new SqlConnection(_connection.GetConnection());

        int result = db.Execute(SqlQueries.UpdateQuery, model);
        var message = result > 0 ? "Update Successful." : "Update Failed.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel model)
    {
        var item = GetById(id);
        if (item == null) return NotFound("No Data Found.");

        string conditions = string.Empty;

        if (!string.IsNullOrEmpty(model.BlogTitle))
        {
            conditions += " [BlogTitle] = @BlogTitle, ";
        }
        if (!string.IsNullOrEmpty(model.BlogAuthor))
        {
            conditions += " [BlogAuthor] = @BlogAuthor, ";
        }
        if (!string.IsNullOrEmpty(model.BlogContent))
        {
            conditions += " [BlogContent] = @BlogContent, ";
        }
        if (conditions.Length > 0)
        {
            return NotFound("No Data To Update");
        }
        conditions = conditions.Substring(0, conditions.Length - 2);

        string query = $@"UPDATE Tbl_Blog SET {conditions} WHERE BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(_connection.GetConnection());

        int result = db.Execute(query, model);
        var message = result > 0 ? "Update Successful." : "Update Failed.";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return NotFound("No Data Found.");

        using IDbConnection db = new SqlConnection(_connection.GetConnection());

        int result = db.Execute(SqlQueries.DeleteQuery, new { BlogId = id });
        var message = result > 0 ? "Delete Successful." : "Delete Failed.";
        return Ok(message);
    }

    private BlogModel GetById(int id)
    {
        using IDbConnection db = new SqlConnection(_connection.GetConnection());
        var item = db.Query<BlogModel>(SqlQueries.EditQuery, new { BlogId = id }).FirstOrDefault();
        return item!;
    }
}