using MPTDotNetCore.Shared.Models;
using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.ConsoleApp.Features.AdoDotNet;

public class AdoDotNetExampleV3
{
    private string _connection;

    public AdoDotNetExampleV3(string connection)
    {
        _connection = connection;
    }

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
        var connection = new SqlConnection(_connection); connection.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(StaticModel.SelectQuery, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                PrintDataTable(dt);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();
    }

    private void Edit()
    {
        int id = GetValidId("Enter the Blog Id");
        Console.WriteLine("===============================");

        if (id == -1) return;

        var connection = new SqlConnection(_connection); connection.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(StaticModel.EditQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        Console.WriteLine("No data found for the specified Blog Id.");
                        return;
                    }

                    DataRow dr = dt.Rows[0];
                    BlogModel blog = new BlogModel
                    {
                        BlogId = id,
                        BlogTitle = dr.Field<string>(StaticModel.Title),
                        BlogAuthor = dr.Field<string>(StaticModel.Author),
                        BlogContent = dr.Field<string>(StaticModel.Content)
                    };

                    Console.WriteLine($"Current Blog Details:");
                    Console.WriteLine($"BlogTitle: {blog.BlogTitle}");
                    Console.WriteLine($"BlogAuthor: {blog.BlogAuthor}");
                    Console.WriteLine($"BlogContent: {blog.BlogContent}");

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();
    }

    private void Create()
    {
        BlogModel newBlog = GetBlogDetailsFromUser(new BlogModel());

        var connection = new SqlConnection(_connection); connection.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(StaticModel.CreateQuery, connection))
            {
                AddModelParameters(cmd, newBlog);
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? "Create Successful." : "Create Failed.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();
    }

    private void Update()
    {
        int id = GetValidId("Enter the Blog Id to update");

        if (id == -1) return;

        BlogModel existingBlog = GetBlogById(id);

        if (existingBlog == null)
        {
            Console.WriteLine("Blog not found for the specified ID."); return;
        }

        BlogModel updatedBlog = GetBlogDetailsFromUser(existingBlog);

        var connection = new SqlConnection(_connection); connection.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(StaticModel.UpdateQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);
                cmd.Parameters.AddWithValue("@BlogTitle", updatedBlog.BlogTitle);
                cmd.Parameters.AddWithValue("@BlogAuthor", updatedBlog.BlogAuthor);
                cmd.Parameters.AddWithValue("@BlogContent", updatedBlog.BlogContent);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Update Successful.");
                }
                else
                {
                    Console.WriteLine("Update Failed. No rows affected.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();
    }

    private void Delete()
    {
        int id = GetValidId("Enter the Blog Id to delete");

        if (id == -1) return;

        BlogModel existingBlog = GetBlogById(id);

        if (existingBlog == null)
        {
            Console.WriteLine("Blog not found for the specified ID."); return;
        }

        var connection = new SqlConnection(_connection); connection.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(StaticModel.DeleteQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);
                int result = cmd.ExecuteNonQuery();
                Console.WriteLine(result > 0 ? "Delete Successful." : "Delete Failed. No rows affected.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();
    }

    #region Validation

    private BlogModel GetBlogById(int id)
    {
        BlogModel blog = null!;

        var connection = new SqlConnection(_connection); connection.Open();

        try
        {
            using (SqlCommand cmd = new SqlCommand(StaticModel.EditQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        blog = new BlogModel
                        {
                            BlogId = id,
                            BlogTitle = dr.Field<string>(StaticModel.Title),
                            BlogAuthor = dr.Field<string>(StaticModel.Author),
                            BlogContent = dr.Field<string>(StaticModel.Content)
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        connection.Close();

        return blog;
    }

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

    private void PrintDataTable(DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"BlogId : {row[StaticModel.Id]}");
            Console.WriteLine($"BlogTitle : {row[StaticModel.Title]}");
            Console.WriteLine($"BlogAuthor : {row[StaticModel.Author]}");
            Console.WriteLine($"BlogContent : {row[StaticModel.Content]}");
            Console.WriteLine("===============================");
        }
    }

    private void AddModelParameters(SqlCommand command, BlogModel model)
    {
        command.Parameters.AddWithValue("@BlogId", model.BlogId);
        command.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
        command.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
        command.Parameters.AddWithValue("@BlogContent", model.BlogContent);
    }

    #endregion
}