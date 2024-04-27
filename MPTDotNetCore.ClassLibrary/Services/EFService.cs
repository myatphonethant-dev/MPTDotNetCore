using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;

namespace MPTDotNetCore.Shared.Services;

public class EFService<TModel> where TModel : class
{
    #region Constructor

    private readonly AppDbContext _db;

    public EFService(AppDbContext db)
    {
        _db = db;
    }

    #endregion

    #region GetById 

    public TModel GetById(int id)
    {
        var item = _db.Set<TModel>().Find(id);
        return item!;
    }

    #endregion

    #region Result Message

    public void Result(string operation)
    {
        int result = _db.SaveChanges();
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