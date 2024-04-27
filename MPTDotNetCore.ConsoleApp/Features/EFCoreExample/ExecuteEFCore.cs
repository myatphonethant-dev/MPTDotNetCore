using MPTDotNetCore.Shared.DbServices;

namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public static class ExecuteEFCore
{
    #region Static Startup (Program.cs )

    public static void Execute()
    {
        DbService connectionString = new();
        AppDbContext _dbService = new AppDbContext(connectionString);

        EFCoreExample efCoreExample = new EFCoreExample(_dbService);
        EFCoreExampleV2 efCoreExampleV2 = new EFCoreExampleV2(_dbService);

        //efCoreExample.Run();
        efCoreExampleV2.Run();
    }

    #endregion
}