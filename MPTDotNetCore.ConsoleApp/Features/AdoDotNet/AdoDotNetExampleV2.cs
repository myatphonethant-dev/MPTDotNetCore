using MPTDotNetCore.Shared.Models;
using System.Data;
using System.Data.SqlClient;

namespace MPTDotNetCore.ConsoleApp.Features.AdoDotNet
{
    public class AdoDotNetExampleV2
    {
        private string _connection;

        public AdoDotNetExampleV2(string connection)
        {
            _connection = connection;
        }

        public void Run()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void Execute(string query, Action<SqlCommand> addParameters = null!)
        {
            try
            {
                var connection = new SqlConnection(_connection); connection.Open();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    addParameters?.Invoke(cmd);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine($"Id : {dr[StaticModel.Id]}");
                            Console.WriteLine($"Title : {dr[StaticModel.Title]}");
                            Console.WriteLine($"Author : {dr[StaticModel.Author]}");
                            Console.WriteLine($"Content : {dr[StaticModel.Content]}");
                            Console.WriteLine("===============================");
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Read()
        {
            Execute(StaticModel.SelectQuery);
        }

        public void Edit()
        {
            int id = ValidateId();
            Execute(StaticModel.EditQuery, cmd => cmd.Parameters.AddWithValue("@BlogId", id));
        }

        public void Create()
        {
            try
            {
                string title = ValidateString("Enter the Blog Title");
                string author = ValidateString("Enter the Blog Author");
                string content = ValidateString("Enter the Blog Content");

                Execute(StaticModel.CreateQuery, cmd =>
                {
                    cmd.Parameters.AddWithValue("@BlogTitle", title);
                    cmd.Parameters.AddWithValue("@BlogAuthor", author);
                    cmd.Parameters.AddWithValue("@BlogContent", content);
                });

                Console.WriteLine("Blog created successfully.");
                Console.WriteLine("===============================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create Blog. {ex.Message}");
            }
        }

        public void Update()
        {
            try
            {
                int id = ValidateId();
                string title = ValidateString("Enter the Blog Title");
                string author = ValidateString("Enter the Blog Author");
                string content = ValidateString("Enter the Blog Content");

                Execute(StaticModel.UpdateQuery, cmd =>
                {
                    cmd.Parameters.AddWithValue("@BlogId", id);
                    cmd.Parameters.AddWithValue("@BlogTitle", title);
                    cmd.Parameters.AddWithValue("@BlogAuthor", author);
                    cmd.Parameters.AddWithValue("@BlogContent", content);
                });
                Console.WriteLine("Blog update successfully.");
                Console.WriteLine("===============================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update Blog. {ex.Message}");
            }
        }

        public void Delete()
        {
            try
            {
                int id = ValidateId();
                Execute(StaticModel.DeleteQuery, cmd => cmd.Parameters.AddWithValue("@BlogId", id));
                Console.WriteLine("Blog delete successfully.");
                Console.WriteLine("===============================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete Blog. {ex.Message}");
            }
        }

        private int ValidateId()
        {
            int id;
            bool isValidId = false;

            do
            {
                Console.Write("Enter the Blog Id : ");
                string idInput = Console.ReadLine()!;

                if (int.TryParse(idInput, out id))
                {
                    if (IdExists(id))
                    {
                        isValidId = true;
                    }
                    else
                    {
                        Console.WriteLine("Blog with the specified ID does not exist. Please enter a valid ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
            } while (!isValidId);

            return id;
        }

        private bool IdExists(int id)
        {
            var connection = new SqlConnection(_connection); connection.Open();

            using (SqlCommand cmd = new SqlCommand(StaticModel.EditQuery, connection))
            {
                cmd.Parameters.AddWithValue("@BlogId", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        return count > 0;
                    }
                }
            }

            return false;
        }

        private string ValidateString(string message)
        {
            string input;
            do
            {
                Console.Write($"{message} : ");
                input = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Value must not be null or whitespace! Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
    }
}