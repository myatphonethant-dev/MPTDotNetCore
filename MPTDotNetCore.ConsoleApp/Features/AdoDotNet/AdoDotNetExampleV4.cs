using MPTDotNetCore.Shared.Models;
using MPTDotNetCore.Shared.Services;

public class AdoDotNetExampleV4 : IAdoExample
{
    private readonly AdoService _adoService;
    private readonly MainLayout _mainLayout;
    private string _connection;

    public AdoDotNetExampleV4(AdoService adoService, string connection, MainLayout mainLayout)
    {
        _adoService = adoService;
        _connection = connection;
        _mainLayout = mainLayout;
    }

    public void Read()
    {
        var blogLst = _adoService.GetLst<List<BlogModel>>(SqlQueries.SelectQuery);

        _mainLayout.ResultMessage(blogLst);
    }

    public void Edit(int blogId)
    {
        var blogById = new { BlogId = blogId };
            
        var blog = _adoService.GetItem<BlogModel>(SqlQueries.EditQuery, blogById.ToDictionary());

        _mainLayout.ResultMessage(blog);
    }

    public void Create(string title, string author, string content)
    {
        var blogCreate = new
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        var result = _adoService.Execute(SqlQueries.CreateQuery, blogCreate.ToDictionary());

        _mainLayout.ResultMessage(result, "Create");
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

        var result = _adoService.Execute(SqlQueries.UpdateQuery, blogUpdate.ToDictionary());

        _mainLayout.ResultMessage(result, "Update");
    }

    public void Delete(int blogId)
    {
        var blogDelete = new { BlogId = blogId };

        var result = _adoService.Execute(SqlQueries.DeleteQuery, blogDelete.ToDictionary());

        _mainLayout.ResultMessage(result, "Delete");
    }
}
