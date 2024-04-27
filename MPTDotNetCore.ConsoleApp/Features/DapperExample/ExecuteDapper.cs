using MPTDotNetCore.Shared.Models;
using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.DapperExample;

public static class ExecuteDapper
{
    #region Static Startup (Program.cs )

    public static void Execute()
    {
        DbService dbService = new();
        string connection = dbService.GetConnection();
        DapperService<BlogModel> dapperService = new DapperService<BlogModel>(connection);

        DapperExample dapperExample = new DapperExample(connection);
        DapperExampleV2 dapperExampleV2 = new DapperExampleV2(connection);
        DapperExampleV3 dapperExampleV3 = new DapperExampleV3(dapperService, connection);

        //dapperExample.Run();
        //dapperExampleV2.Run();

        IBaseExample adoExample = dapperExampleV3;

        MainLayout mainLayout = new MainLayout(adoExample);

        mainLayout.Run();
    }

    #endregion
}