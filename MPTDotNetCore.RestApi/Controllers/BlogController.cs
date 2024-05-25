using Microsoft.AspNetCore.Mvc;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;

namespace MPTDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly AppDbContext _db;

    public BlogController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Read()
    {
        var lst = _db.TblBlogs.ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

        if (item == null) return NotFound("No Data Found.");
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(BlogModel model)
    {
        _db.TblBlogs.Add(model);
        var result = _db.SaveChanges();

        var message = result > 0 ? "Saving Successful." : "Saving Failed.";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel model)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

        if (item == null) return NotFound("No Data Found.");

        item.BlogTitle = model.BlogTitle;
        item.BlogAuthor = model.BlogAuthor;
        item.BlogContent = model.BlogContent;
        var result = _db.SaveChanges();

        var message = result > 0 ? "Update Successful." : "Update Failed.";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel model)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

        if (item == null) return NotFound("No Data Found.");

        if (string.IsNullOrEmpty(item.BlogTitle)) item.BlogTitle = model.BlogTitle;
        if (string.IsNullOrEmpty(item.BlogAuthor)) item.BlogTitle = model.BlogAuthor;
        if (string.IsNullOrEmpty(item.BlogContent)) item.BlogTitle = model.BlogContent;
        var result = _db.SaveChanges();

        var message = result > 0 ? "Update Successful." : "Update Failed.";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

        if (item == null) return NotFound("No Data Found.");

        _db.TblBlogs.Remove(item);
        var result = _db.SaveChanges();

        var message = result > 0 ? "Delete Successful." : "Delete Failed.";
        return Ok(message);
    }
}