namespace MPTDotNetCore.Shared.Models;

public static class StaticModel
{
    public const string Id = "BlogId";

    public const string Title = "BlogTitle";

    public const string Author = "BlogAuthor";

    public const string Content = "BlogContent";


    public static string SelectQuery = @"SELECT * FROM Tbl_Blog";

    public static string CreateQuery = @"INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent) VALUES(@BlogTitle, @BlogAuthor, @BlogContent)";

    public static string EditQuery = @"SELECT * FROM Tbl_Blog Where BlogId = @BlogId";

    public static string UpdateQuery = @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent] = @BlogContent WHERE BlogId = @BlogId";

    public static string DeleteQuery = @"Delete From Tbl_Blog WHERE BlogId = @BlogId";
}