using Dapper;
using MPTDotNetCore.Shared.Models;
using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.ConsoleApp.Features.DapperExample;

public class DapperExample
{
    private string _connection;

    public DapperExample(string connection)
    {
        _connection = connection;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Read");
            Console.WriteLine("2. Edit");
            Console.WriteLine("3. Create");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Exit");
            Console.WriteLine("===============================");

            string input;
            do
            {
                Console.Write("Enter your choice: ");
                input = Console.ReadLine()!;
                Console.WriteLine("===============================");
            } while (string.IsNullOrEmpty(input));

            switch (input)
            {
                case "1":
                    Read();
                    break;
                case "2":
                    Edit();
                    break;
                case "3":
                    Create();
                    break;
                case "4":
                    Update();
                    break;
                case "5":
                    Delete();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                    Console.WriteLine("===============================");
                    break;
            }
        }
    }

    public void Read()
    {
        using IDbConnection _db = new SqlConnection(_connection);
        List<BlogModel> lst = _db.Query<BlogModel>(SqlQueries.SelectQuery).ToList();

        foreach (BlogModel item in lst)
        {
            Console.WriteLine($"Id : {item.BlogId}");
            Console.WriteLine($"Title : {item.BlogTitle}");
            Console.WriteLine($"Author : {item.BlogAuthor}");
            Console.WriteLine($"Content : {item.BlogContent}");
            Console.WriteLine("===============================");
        }
    }

    public void Edit()
    {
        try
        {
            int id;
            bool isValidId = false;

            do
            {
                Console.Write("Enter the Blog Id : ");
                string idInput = Console.ReadLine()!;

                if (int.TryParse(idInput, out id))
                {
                    isValidId = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
            } while (!isValidId);

            using IDbConnection _db = new SqlConnection(_connection);
            var item = _db.Query<BlogModel>(SqlQueries.EditQuery, new BlogModel { BlogId = id }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            Console.WriteLine($"Id : {item.BlogId}");
            Console.WriteLine($"Title : {item.BlogTitle}");
            Console.WriteLine($"Author : {item.BlogAuthor}");
            Console.WriteLine($"Content : {item.BlogContent}");
            Console.WriteLine("===============================");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void Create()
    {
        try
        {
            string title;
            string author;
            string content;
            do
            {
                Console.Write("Enter the Blog Title : ");
                title = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            do
            {
                Console.Write("Enter the Blog Author : ");
                author = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(author));

            do
            {
                Console.Write("Enter the Blog Content : ");
                content = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(content));

            var item = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            using IDbConnection _db = new SqlConnection(_connection);
            int result = _db.Execute(SqlQueries.CreateQuery, item);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
            Console.WriteLine("===============================");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void Update()
    {
        try
        {
            int id;
            string title;
            string author;
            string content;
            bool isValidId = false;

            do
            {
                Console.Write("Enter the Blog Id : ");
                string idInput = Console.ReadLine()!;

                if (int.TryParse(idInput, out id))
                {
                    isValidId = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
            } while (!isValidId);

            do
            {
                Console.Write("Enter the Blog Title : ");
                title = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(title));

            do
            {
                Console.Write("Enter the Blog Author : ");
                author = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(author))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(author));

            do
            {
                Console.Write("Enter the Blog Content : ");
                content = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(content));

            var item = new BlogModel
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            using IDbConnection _db = new SqlConnection(_connection);
            int result = _db.Execute(SqlQueries.UpdateQuery, item);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
            Console.WriteLine("===============================");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void Delete()
    {
        try
        {
            int id;
            bool isValidId = false;

            do
            {
                Console.Write("Enter the Blog Id : ");
                string idInput = Console.ReadLine()!;

                if (int.TryParse(idInput, out id))
                {
                    isValidId = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
            } while (!isValidId);

            using IDbConnection _db = new SqlConnection(_connection);
            int result = _db.Execute(SqlQueries.DeleteQuery, new BlogModel { BlogId = id });

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
            Console.WriteLine("===============================");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}