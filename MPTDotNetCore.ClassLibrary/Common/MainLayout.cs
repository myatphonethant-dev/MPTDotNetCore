﻿using System.Reflection;

namespace MPTDotNetCore.Shared.Common;

#region Common Interface

public interface IBaseExample
{
    public void Read();
    public void Edit(int id);
    public void Create(string title, string author, string content);
    public void Update(int id, string title, string author, string content);
    public void Delete(int id);
}

#endregion

public class MainLayout
{
    #region Constructor

    private readonly IBaseExample _example;

    public MainLayout(IBaseExample example)
    {
        _example = example;
    }

    #endregion

    #region Menu Operator

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

    #endregion

    #region Execute Operator Command

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
            MethodInfo method = typeof(IBaseExample).GetMethod(methodName)!;

            if (method != null)
            {
                try
                {
                    if (methodName == "Read")
                    {
                        method.Invoke(_example, null);
                    }
                    else if (methodName == "Edit" || methodName == "Update" || methodName == "Delete")
                    {
                        int id = GetIdFromUserInput("Enter BlogId :");

                        if (methodName == "Edit" || methodName == "Delete")
                        {
                            method.Invoke(_example, new object[] { id });
                        }
                        else if (methodName == "Update")
                        {
                            string title = GetUserInput("Enter BlogTitle: ");
                            string author = GetUserInput("Enter BlogAuthor: ");
                            string content = GetUserInput("Enter BlogContent: ");
                            method.Invoke(_example, new object[] { id, title, author, content });
                        }
                    }
                    else if (methodName == "Create")
                    {
                        string title = GetUserInput("Enter BlogTitle: ");
                        string author = GetUserInput("Enter BlogAuthor: ");
                        string content = GetUserInput("Enter BlogContent: ");
                        method.Invoke(_example, new object[] { title, author, content });
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

    #endregion

    #region Validation Methods

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

    #endregion
}