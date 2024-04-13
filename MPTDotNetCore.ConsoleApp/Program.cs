using MPTDotNetCore.ConsoleApp;
using System.Data.SqlClient;

AdoDotNetExample adoDotNetExample = new AdoDotNetExample(StaticClass.connection);
AdoDotNetExampleV2 adoDotNetExampleV2 = new AdoDotNetExampleV2(StaticClass.connection);

//adoDotNetExample.Run();
adoDotNetExampleV2.Run();

Console.ReadKey();