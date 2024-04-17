using MPTDotNetCore.Shared.Models;
using MPTDotNetCore.Shared.Services;

public class AdoDotNetExampleV4 : IAdoExample
{
    private readonly AdoService _adoService;
    private string _connection;

    public AdoDotNetExampleV4(AdoService adoService, string connection)
    {
        _adoService = adoService;
        _connection = connection;
    }

    public void Read()
    {
        var blogLst = _adoService.GetLst<List<BlogModel>>(SqlQueries.SelectQuery);

        ResultMessage(blogLst);
    }

    public void Edit(int blogId)
    {
        var blogById = new
        {
            BlogId = blogId
        };

        var blog = _adoService.GetItem<BlogModel>(SqlQueries.EditQuery, blogById.ToDictionary());

        ResultMessage(blog);
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

        ResultMessage(result, "Create");
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

        ResultMessage(result, "Update");
    }

    public void Delete(int blogId)
    {
        var blogDelete = new
        {
            BlogId = blogId
        };

        var result = _adoService.Execute(SqlQueries.DeleteQuery, blogDelete.ToDictionary());

        ResultMessage(result, "Delete");
    }

    private void ResultMessage<T>(T result)
    {
        Console.WriteLine(result!.ToJson());
    }

    private void ResultMessage(int result, string operation)
    {
        Console.WriteLine(result > 0 ? $"{operation} Successful." : $"{operation} Failed. No rows affected.");
    }
}
