using MPTDotNetCore.Shared.Models;
using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.DapperExample;

public class DapperExampleV3 : IBaseExample 
{
    #region Constructor

    private readonly DapperService<BlogModel> _dapperService;
    private string _connection;

    public DapperExampleV3(DapperService<BlogModel> dapperService, string connection)
    {
        _dapperService = dapperService;
        _connection = connection;
    }

    #endregion

    public void Read()
    {
        var lst = _dapperService.Query(SqlQueries.SelectQuery);

        _dapperService.DataList(lst);
    }

    public void Edit(int blogId)
    {
        var blogById = new { BlogId = blogId };

        var item = _dapperService.QuerySingle(SqlQueries.EditQuery, blogById);

        _dapperService.DataList(item);
    }

    public void Create(string title, string author, string content)
    {
        var blogCreate = new
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        _dapperService.Execute(SqlQueries.CreateQuery, "Creating", blogCreate);
    }

    public void Update(int blogId, string title, string author, string content)
    {
        var blogUpdate = new
        {
            BlogId = blogId,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        _dapperService.Execute(SqlQueries.UpdateQuery, "Updating", blogUpdate);
    }

    public void Delete(int blogId)
    {
        var blogDelete = new { BlogId = blogId };

        _dapperService.Execute(SqlQueries.DeleteQuery, "Deleting", blogDelete);
    }
}