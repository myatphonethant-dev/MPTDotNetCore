using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.ConsoleApp;

public class AdoDotNetExample
{
    private readonly SqlConnection connection;

    public AdoDotNetExample(SqlConnection connection)
    {
        this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
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
        try
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            using (SqlCommand cmd = new SqlCommand(StaticClass.SelectQuery, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine($"Id : {dr[StaticClass.Id]}");
                    Console.WriteLine($"Title : {dr[StaticClass.Title]}");
                    Console.WriteLine($"Author : {dr[StaticClass.Author]}");
                    Console.WriteLine($"Content : {dr[StaticClass.Content]}");
                    Console.WriteLine("===============================");
                }
            }

            connection.Close();
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

            if (connection.State != ConnectionState.Open) connection.Open();

            using (SqlCommand cmd = new SqlCommand(StaticClass.EditQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        Console.WriteLine("No data found.");
                        return;
                    }
                    DataRow dr = dt.Rows[0];
                    Console.WriteLine($"Title: {dr["BlogTitle"]}");
                    Console.WriteLine($"Author: {dr["BlogAuthor"]}");
                    Console.WriteLine($"Content: {dr["BlogContent"]}");
                    Console.WriteLine("===============================");
                }
            }

            connection.Close();
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

            if (connection.State != ConnectionState.Open) connection.Open();

            using (SqlCommand cmd = new SqlCommand(StaticClass.CreateQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", title);
                cmd.Parameters.AddWithValue("@BlogAuthor", author);
                cmd.Parameters.AddWithValue("@BlogContent", content);
                int result = cmd.ExecuteNonQuery();
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                Console.WriteLine(message);
                Console.WriteLine("===============================");
            }

            connection.Close();
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

            if (connection.State != ConnectionState.Open) connection.Open();

            using (SqlCommand cmd = new SqlCommand(StaticClass.UpdateQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);
                cmd.Parameters.AddWithValue("@BlogTitle", title);
                cmd.Parameters.AddWithValue("@BlogAuthor", author);
                cmd.Parameters.AddWithValue("@BlogContent", content);
                Console.WriteLine("===============================");
                int result = cmd.ExecuteNonQuery();
                string message = result > 0 ? "Update Successful." : "Update Failed.";
                Console.WriteLine(message);
                Console.WriteLine("===============================");
            }

            connection.Close();
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

            if (connection.State != ConnectionState.Open) connection.Open();

            using (SqlCommand cmd = new SqlCommand(StaticClass.DeleteQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);
                int result = cmd.ExecuteNonQuery();
                string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
                Console.WriteLine(message);
                Console.WriteLine("===============================");
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}