using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.EFCoreExample;

public static class ExecuteEFCore
{
    #region Static Startup (Program.cs )

    public static void Execute()
    {
        DbService dbService = new();
        string connection = dbService.GetConnection();

        EFCoreExample efCoreExample = new EFCoreExample(connection);

        efCoreExample.Run();
    }

    #endregion
}