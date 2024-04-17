using System.Reflection;

namespace MPTDotNetCore.Shared.Services
{
    public interface IAdoExample
    {
        void Read();
        void Edit(int blogId);
        void Create(string title, string author, string content);
        void Update(int blogId, string title, string author, string content);
        void Delete(int blogId);
    }

    public class MainLayout
    {
        private readonly IAdoExample _adoExample;

        public MainLayout(IAdoExample adoExample)
        {
            _adoExample = adoExample;
        }

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                string input = GetUserInput("Choose an operation (1-6)");

                if (ValidateInput(input))
                {
                    ExecuteCommand(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
                }
            }
        }

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

        private void ExecuteCommand(string input)
        {
            string? methodName = input switch
            {
                "1" => "Read",
                "2" => "Edit",
                "3" => "Create",
                "4" => "Update",
                "5" => "Delete",
                "6" => "ExitApp",
                _ => null
            };

            if (methodName == "ExitApp")
            {
                Environment.Exit(0);
            }
            else if (!string.IsNullOrEmpty(methodName))
            {
                MethodInfo method = typeof(IAdoExample).GetMethod(methodName)!;

                if (method != null)
                {
                    try
                    {
                        if (methodName == "Read")
                        {
                            method.Invoke(_adoExample, null);
                        }
                        else if (methodName == "Edit" || methodName == "Update" || methodName == "Delete")
                        {
                            int id = GetIdFromUserInput("Enter BlogId :");

                            if (methodName == "Edit" || methodName == "Delete")
                            {
                                method.Invoke(_adoExample, new object[] { id });
                            }
                            else if (methodName == "Update")
                            {
                                string title = GetUserInput("Enter BlogTitle: ");
                                string author = GetUserInput("Enter BlogAuthor: ");
                                string content = GetUserInput("Enter BlogContent: ");
                                method.Invoke(_adoExample, new object[] { id, title, author, content });
                            }
                        }
                        else if (methodName == "Create")
                        {
                            string title = GetUserInput("Enter BlogTitle: ");
                            string author = GetUserInput("Enter BlogAuthor: ");
                            string content = GetUserInput("Enter BlogContent: ");
                            method.Invoke(_adoExample, new object[] { title, author, content });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Method {methodName} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 6.");
            }

            Console.WriteLine("===============================");
        }

        private bool ValidateInput(string input)
        {
            return input.Length == 1 && char.IsDigit(input[0]) && input[0] >= '1' && input[0] <= '6';
        }

        private int GetIdFromUserInput(string message)
        {
            string input;
            int id;

            do
            {
                Console.Write($"{message} ");
                input = Console.ReadLine()!;
            } while (!int.TryParse(input, out id));

            return id;
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

        public void ResultMessage<T>(T result)
        {
            Console.WriteLine(result!.ToJson());
        }

        public void ResultMessage(int result, string operation)
        {
            Console.WriteLine(result > 0 ? $"{operation} Successful." : $"{operation} Failed. No rows affected.");
        }
    }
}
