using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.AdoDotNet;

public static class AdoDotNet
{
    public static void ExecuteProgram()
    {
        DbService dbService = new();
        string connection = dbService.GetConnection();
        AdoService adoService = new AdoService(connection);

        AdoDotNetExample adoDotNetExample = new(connection);
        AdoDotNetExampleV2 adoDotNetExampleV2 = new(connection);
        AdoDotNetExampleV3 adoDotNetExampleV3 = new(connection);
        AdoDotNetExampleV4 adoDotNetExampleV4 = new(adoService, connection);

        IAdoExample adoExample = adoDotNetExampleV4;

        //adoDotNetExample.Run();
        //adoDotNetExampleV2.Run();
        //adoDotNetExampleV3.Run();

        MainLayout mainLayout = new MainLayout(adoExample);

        mainLayout.Run();
    }
}