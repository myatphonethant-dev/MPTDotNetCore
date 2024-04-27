using Dapper;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;
using System.Data;

namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public class EFCoreExampleV2
{
    #region Constructor

    private readonly AppDbContext _db;

    public EFCoreExampleV2(AppDbContext db)
    {
        _db = db;
    }

    #endregion

    public void Run()
    {
        try
        {
            while (true)
            {
                DisplayMenu();

                string input = GetUserInput("Enter your choice");

                if (string.IsNullOrEmpty(input) || !ValidateInput(input))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                UserInput(input);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void Read()
    {
        try
        {
            List<BlogModel> lst = _db.TblBlogs.ToList();

            foreach (BlogModel item in lst)
            {
                DataList(item);
                Console.WriteLine("===============================");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void Edit()
    {
        int id = GetValidId("Enter the Blog Id");
        Console.WriteLine("===============================");

        if (id == -1) return;

        try
        {
            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            DataList(item!);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void Create()
    {
        BlogModel newBlog = GetBlogDetailsFromUser(new BlogModel());

        try
        {
            _db.Add(newBlog);
            int result = _db.SaveChanges();

            Result(result, "Saving");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void Update()
    {
        int id = GetValidId("Enter the Blog Id to update");

        if (id == -1) return;

        try
        {
            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            BlogModel updatedBlog = GetBlogDetailsFromUser(item!);

            item.BlogTitle = updatedBlog.BlogTitle;
            item.BlogAuthor = updatedBlog.BlogAuthor;
            item.BlogContent = updatedBlog.BlogContent;

            int result = _db.SaveChanges();

            Result(result, "Updating");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private void Delete()
    {
        int id = GetValidId("Enter the Blog Id to delete");

        if (id == -1) return;

        try
        {
            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                return;
            }

            _db.TblBlogs.Remove(item);
            int result = _db.SaveChanges();

            Result(result, "Deleting");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    #region Validation

    private int GetValidId(string message)
    {
        int id = 0;
        bool isValidId = false;

        do
        {
            string idInput = GetUserInput(message);

            if (string.IsNullOrEmpty(idInput))
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
            }
            else if (int.TryParse(idInput, out id))
            {
                isValidId = true;
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
            }
        } while (!isValidId);

        return id;
    }

    private BlogModel GetBlogDetailsFromUser(BlogModel currentBlog)
    {
        BlogModel updatedBlog = new BlogModel();

        Console.WriteLine($"Enter new details for the Blog (Press Enter to keep existing value):");
        updatedBlog.BlogId = currentBlog.BlogId;
        updatedBlog.BlogTitle = GetUserInput($"Enter new BlogTitle (Current: {currentBlog.BlogTitle}): ") ?? currentBlog.BlogTitle;
        updatedBlog.BlogAuthor = GetUserInput($"Enter new BlogAuthor (Current: {currentBlog.BlogAuthor}): ") ?? currentBlog.BlogAuthor;
        updatedBlog.BlogContent = GetUserInput($"Enter new BlogContent (Current: {currentBlog.BlogContent}): ") ?? currentBlog.BlogContent;

        return updatedBlog;
    }

    private void Result(int result, string operation)
    {
        string message = result > 0 ? $"{operation} Successful." : $"{operation} Failed.";
        Console.WriteLine(message);
    }

    #endregion

    #region User Input

    private void DisplayMenu()
    {
        Console.WriteLine("Choose an operation:");
        Console.WriteLine("1. Read");
        Console.WriteLine("2. Edit");
        Console.WriteLine("3. Create");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("6. Exit");
        Console.WriteLine("===============================");
    }

    private string GetUserInput(string message)
    {
        string input;
        do
        {
            Console.Write($"{message} : ");
            input = Console.ReadLine()!;
        } while (string.IsNullOrEmpty(input));

        return input;
    }

    private bool ValidateInput(string input)
    {
        return input.Length == 1 && char.IsDigit(input[0]) && input[0] >= '1' && input[0] <= '6';
    }

    private void UserInput(string input)
    {
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
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                break;
        }
        Console.WriteLine("===============================");
    }

    private void DataList(BlogModel item)
    {
        if (item == null)
        {
            return;
        }

        Console.WriteLine($"Id: {item.BlogId}");
        Console.WriteLine($"Title: {item.BlogTitle}");
        Console.WriteLine($"Author: {item.BlogAuthor}");
        Console.WriteLine($"Content: {item.BlogContent}");
    }

    #endregion
}