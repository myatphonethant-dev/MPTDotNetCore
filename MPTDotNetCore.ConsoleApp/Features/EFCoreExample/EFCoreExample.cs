namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public class EFCoreExample
{
    private string _connection;

    public EFCoreExample(string connection)
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

    }

    public void Edit()
    {

    }

    public void Create()
    {

    }

    public void Update()
    {

    }

    public void Delete()
    {

    }
}