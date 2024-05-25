namespace MPTDotNetCore.RestApiWithNLayer.Features.Blog;

public class DA_Blog
{
    private readonly AppDbContext _db;

    public DA_Blog(AppDbContext db)
    {
        _db = db;
    }

    public List<BlogModel> GetBlogs()
    {
        var lst = _db.TblBlogs.ToList();
        return lst;
    }

    public BlogModel GetBlog(int id)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
        return item!;
    }

    public int CreateBlog(BlogModel requestModel)
    {
        _db.TblBlogs.Add(requestModel);
        var result = _db.SaveChanges();
        return result;
    }

    public int UpdateBlog(int id, BlogModel requestModel)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;

        var result = _db.SaveChanges();
        return result;
    }

    public int DeleteBlog(int id)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        _db.TblBlogs.Remove(item);
        var result = _db.SaveChanges();
        return result;
    }
}
