using MPTDotNetCore.ClassLibrary.Models;
using MPTDotNetCore.ClassLibrary.Services;
using MPTDotNetCore.ConsoleApp.AdoDotNet;
using System.Data.SqlClient;

DbService connection = new DbService();

var net = connection.GetConnection();

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
AdoDotNetExampleV2 adoDotNetExampleV2 = new AdoDotNetExampleV2();
AdoDotNetExampleV3 adoDotNetExampleV3 = new AdoDotNetExampleV3();

//adoDotNetExample.Run();
//adoDotNetExampleV2.Run();
adoDotNetExampleV3.Run();

Console.ReadKey();