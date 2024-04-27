using MPTDotNetCore.Shared.Common;
using MPTDotNetCore.Shared.DbServices;
using MPTDotNetCore.Shared.Models;
using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public static class ExecuteEFCore
{
    #region Static Startup (Program.cs )

    public static void Execute()
    {
        DbService connectionString = new();
        AppDbContext _dbService = new AppDbContext(connectionString);
        EFService<BlogModel> _efService = new EFService<BlogModel>(_dbService);

        EFCoreExample efCoreExample = new EFCoreExample(_dbService);
        EFCoreExampleV2 efCoreExampleV2 = new EFCoreExampleV2(_dbService);
        EFCoreExampleV3 efCoreExampleV3 = new EFCoreExampleV3(_efService, _dbService);

        //efCoreExample.Run();
        //efCoreExampleV2.Run();

        IBaseExample efExample = efCoreExampleV3;

        MainLayout mainLayout = new MainLayout(efExample);

        mainLayout.Run();
    }

    #endregion
}