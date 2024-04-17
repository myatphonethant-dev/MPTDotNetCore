using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.AdoDotNet;

public static class AdoDotNet
{
    public static void ExecuteProgram()
    {
        DbService dbService = new DbService();
        string connection = dbService.GetConnection();

        AdoDotNetExample adoDotNetExample = new AdoDotNetExample(connection);
        AdoDotNetExampleV2 adoDotNetExampleV2 = new AdoDotNetExampleV2(connection);
        AdoDotNetExampleV3 adoDotNetExampleV3 = new AdoDotNetExampleV3(connection);

        // adoDotNetExample.Run();
        // adoDotNetExampleV2.Run();
        adoDotNetExampleV3.Run();
    }
}