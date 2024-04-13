using System.Data.SqlClient;

namespace MPTDotNetCore.ConsoleApp;

public class StaticClass
{
    public static SqlConnectionStringBuilder stringBuilder;
    public static SqlConnection connection;

    static StaticClass()
    {
        stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sa@123",
        };

        connection = new SqlConnection(stringBuilder.ConnectionString);
    }

    public const string Id = "BlogId";

    public const string Title = "BlogTitle";

    public const string Author = "BlogAuthor";

    public const string Content = "BlogContent";

    public const string SelectQuery = @"SELECT * FROM Tbl_Blog";

    public static string CreateQuery = @"INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent) VALUES(@BlogTitle, @BlogAuthor, @BlogContent)";

    public static string EditQuery = @"SELECT * FROM Tbl_Blog Where BlogId = @BlogId";

    public static string UpdateQuery = @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent] = @BlogContent WHERE BlogId = @BlogId";

    public static string DeleteQuery = @"Delete From Tbl_Blog WHERE BlogId = @BlogId";
}