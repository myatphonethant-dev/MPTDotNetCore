using MPTDotNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Hello, World!");

var httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();