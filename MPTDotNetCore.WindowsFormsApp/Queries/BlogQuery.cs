namespace MPTDotNetCore.WindowsFormsApp.Queries;

internal class BlogQuery
{
    public static string BlogCreate { get; } = @"INSERT INTO Tbl_Blog
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";
}