using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.DapperExample;

public static class ExecuteDapper
{
    #region Static Startup (Program.cs )

    public static void Execute()
    {
        DbService dbService = new();
        string connection = dbService.GetConnection();

        DapperExample dapperExample = new DapperExample(connection);

        dapperExample.Run();
    }

    #endregion
}