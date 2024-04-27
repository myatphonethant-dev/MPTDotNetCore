using MPTDotNetCore.Shared.Common;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.AdoDotNetExample;

public static class ExecuteAdoDotNet
{
    #region Static Startup (Program.cs )

    public static void Execute()
    {
        DbService dbService = new();
        string connection = dbService.GetConnection();
        AdoService adoService = new AdoService(connection);

        AdoDotNetExample adoDotNetExample = new(connection);
        AdoDotNetExampleV2 adoDotNetExampleV2 = new(connection);
        AdoDotNetExampleV3 adoDotNetExampleV3 = new(connection);
        AdoDotNetExampleV4 adoDotNetExampleV4 = new(adoService, connection);

        IBaseExample adoExample = adoDotNetExampleV4;

        //adoDotNetExample.Run();
        //adoDotNetExampleV2.Run();
        //adoDotNetExampleV3.Run();

        MainLayout mainLayout = new MainLayout(adoExample);

        mainLayout.Run();
    }

    #endregion
}