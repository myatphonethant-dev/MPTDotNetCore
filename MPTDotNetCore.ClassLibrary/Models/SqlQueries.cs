namespace MPTDotNetCore.Shared.Models;

public class SqlQueries
{
    public static string SelectQuery { get; } = @"SELECT * FROM Tbl_Blog";

    public static string EditQuery { get; } = @"SELECT * FROM Tbl_Blog Where BlogId = @BlogId";

    public static string CreateQuery { get; } = @"INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent) VALUES(@BlogTitle, @BlogAuthor, @BlogContent)";

    public static string UpdateQuery { get; } = @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle, [BlogAuthor] = @BlogAuthor, [BlogContent] = @BlogContent WHERE BlogId = @BlogId";

    public static string DeleteQuery { get; } = @"DELETE FROM Tbl_Blog WHERE BlogId = @BlogId";
}