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
        _db.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public IActionResult Update()
    {
        return Ok();
    }

    [HttpPatch]
    public IActionResult Patch()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

        if (item == null) return NotFound("No Data Found.");

        _db.Remove(item);
        return Ok(item);
    }
}