using Microsoft.AspNetCore.Mvc;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;
using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNetController : ControllerBase
{
    private readonly DbService _dbService;

    public BlogAdoDotNetController(DbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var connection = new SqlConnection(_dbService.GetConnection());
        connection.Open();

        var cmd = new SqlCommand(SqlQueries.SelectQuery, connection);
        var adapter = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        adapter.Fill(dt);

        connection.Close();

        var lst = dt.AsEnumerable().Select(dr => new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        }).ToList();

        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var connection = new SqlConnection(_dbService.GetConnection());
        connection.Open();

        var cmd = new SqlCommand(SqlQueries.EditQuery, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        var adapter = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        adapter.Fill(dt);

        connection.Close();

        if (dt.Rows.Count == 0) return NotFound("No Data Found.");

        var dr = dt.Rows[0];
        var item = new BlogModel()
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        };

        return Ok(dt);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel model)
    {
        var connection = new SqlConnection(_dbService.GetConnection());
        connection.Open();

        var cmd = new SqlCommand(SqlQueries.CreateQuery, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
        int result = cmd.ExecuteNonQuery();

        connection.Close();
        string message = result > 0 ? "Saving Successful." : "Saving Failed.";

        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult PutBlog(int id, BlogModel blog)
    {
        string getQuery = "SELECT COUNT(*) FROM tbl_blogs WHERE BlogId = @BlogId";

        var connection = new SqlConnection(_dbService.GetConnection());
        connection.Open();

        var checkCmd = new SqlCommand(getQuery, connection);
        checkCmd.Parameters.AddWithValue("@BlogId", id);

        var count = (int)checkCmd.ExecuteScalar();
        if (count == 0) return NotFound();

        var cmd = new SqlCommand(SqlQueries.UpdateQuery, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();

        connection.Close();
        string message = result > 0 ? "Update Successful." : "Update Failed.";

        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blog)
    {
        var connection = new SqlConnection(_dbService.GetConnection());
        connection.Open();

        var cmd = new SqlCommand(SqlQueries.EditQuery, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        var count = (int)cmd.ExecuteScalar();
        if (count == 0) return NotFound();

        var dataAdapter = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        dataAdapter.Fill(dt);

        var lst = new List<BlogModel>();

        if (dt.Rows.Count == 0)
        {
            var response = new { IsSuccess = false, Message = "No data found." };
            return NotFound(response);
        }

        var row = dt.Rows[0];

        var item = new BlogModel
        {
            BlogId = Convert.ToInt32(row["BlogId"]),
            BlogTitle = Convert.ToString(row["BlogTitle"]),
            BlogAuthor = Convert.ToString(row["BlogAuthor"]),
            BlogContent = Convert.ToString(row["BlogContent"]),
        };
        lst.Add(item);

        string conditions = "";
        var parameters = new List<SqlParameter>();

        #region Validation

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            conditions += " [BlogTitle] = @BlogTitle, ";
            parameters.Add(new SqlParameter("@BlogTitle", SqlDbType.NVarChar) { Value = blog.BlogTitle });
            item.BlogTitle = blog.BlogTitle;
        }

        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            conditions += " [BlogAuthor] = @BlogAuthor, ";
            parameters.Add(new SqlParameter("@BlogAuthor", SqlDbType.NVarChar) { Value = blog.BlogAuthor });
            item.BlogAuthor = blog.BlogAuthor;
        }

        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            conditions += " [BlogContent] = @BlogContent, ";
            parameters.Add(new SqlParameter("@BlogContent", SqlDbType.NVarChar) { Value = blog.BlogContent });
            item.BlogContent = blog.BlogContent;
        }

        if (conditions.Length == 0)
        {
            var response = new { IsSuccess = false, Message = "No data found." };
            return NotFound(response);
        }

        #endregion

        conditions = conditions.TrimEnd(',', ' ');
        var query = $@"UPDATE Tbl_Blog SET {conditions} WHERE BlogId = @BlogId";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@BlogId", id);
        command.Parameters.AddRange(parameters.ToArray());

        int result = command.ExecuteNonQuery();

        connection.Close();
        string message = result > 0 ? "Patch Update Successful." : "Patch Updating Failed.";

        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var connection = new SqlConnection(_dbService.GetConnection());
        connection.Open();

        var cmd = new SqlCommand(SqlQueries.DeleteQuery, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();

        connection.Close();
        string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

        return Ok(message);
    }
}
