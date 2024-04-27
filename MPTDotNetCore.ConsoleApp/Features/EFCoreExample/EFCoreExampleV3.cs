using MPTDotNetCore.Shared.Common;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;
using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public class EFCoreExampleV3 : IBaseExample
{
    #region Constructor

    private readonly EFService<BlogModel> _efService;
    private readonly AppDbContext _db;

    public EFCoreExampleV3(EFService<BlogModel> efService, AppDbContext db)
    {
        _efService = efService;
        _db = db;
    }

    #endregion

    public void Read()
    {
        List<BlogModel> blogList = new();

        blogList = _db.TblBlogs.ToList();

        if (blogList is null) Console.WriteLine("No Data Found!");

        foreach (BlogModel item in blogList!)
        {
            _efService.DataList(item);
            Console.WriteLine("===============================");
        }
    }

    public void Edit(int id)
    {
        var item = _efService.GetById(id);

        if (item is null)
        {
            Console.WriteLine("No Data Found!");
            return;
        }

        _efService.DataList(item!);
    }

    public void Create(string title, string author, string content)
    {
        var blogCreate = new BlogModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content,
        };

        _db.TblBlogs.Add(blogCreate);
        _efService.Result("Creating");
    }

    public void Update(int id, string title, string author, string content)
    {
        var item = _efService.GetById(id);

        if (item is null)
        {
            Console.WriteLine("No Data Found!");
            return;
        }

        item.BlogTitle = title;
        item.BlogAuthor = author;
        item.BlogContent = content;

        _efService.Result("Updating");
    }

    public void Delete(int id)
    {
        var item = _efService.GetById(id);

        if (item is null)
        {
            Console.WriteLine("No Data Found!");
            return;
        }

        _db.TblBlogs.Remove(item!);
        _efService.Result("Deleting");
    }
}