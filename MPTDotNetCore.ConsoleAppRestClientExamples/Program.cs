using MPTDotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");

var restClientExample = new RestClientExample();
await restClientExample.RunAsync();

Console.ReadLine();
