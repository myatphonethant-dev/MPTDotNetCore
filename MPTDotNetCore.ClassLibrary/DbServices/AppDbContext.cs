using Microsoft.EntityFrameworkCore;
using MPTDotNetCore.Shared.Models;

namespace MPTDotNetCore.Shared.DbServices;

public class AppDbContext : DbContext
{
    private readonly DbService _db;

    public AppDbContext(DbService db)
    {
        _db = db;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_db.GetConnection());
    }

    public DbSet<BlogModel> TblBlogs { get; set; }
}