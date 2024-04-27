using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;

namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public class EFCoreExample
{
    #region Constructor

    private readonly AppDbContext _db;

    public EFCoreExample(AppDbContext db)
    {
        _db = db;
    }

    #endregion

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
        try
        {
            var lst = _db.TblBlogs.ToList();

            foreach (BlogModel item in lst)
            {
                Console.WriteLine($"Id : {item.BlogId}");
                Console.WriteLine($"Title : {item.BlogTitle}");
                Console.WriteLine($"Author : {item.BlogAuthor}");
                Console.WriteLine($"Content : {item.BlogContent}");
                Console.WriteLine("===============================");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
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

            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                Console.WriteLine("===============================");
                return;
            }

            Console.WriteLine($"Id : {item!.BlogId}");
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

            _db.TblBlogs.Add(item);
            int result = _db.SaveChanges();

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

            var blog = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

            if (blog is null)
            {
                Console.WriteLine("No Data Found!");
                Console.WriteLine("===============================");
                return;
            }

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

            blog!.BlogTitle = title;
            blog.BlogAuthor = author;
            blog.BlogContent = content;

            int result = _db.SaveChanges();

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

            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("No Data Found!");
                Console.WriteLine("===============================");
                return;
            }

            _db.TblBlogs.Remove(item!);
            int result = _db.SaveChanges();

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