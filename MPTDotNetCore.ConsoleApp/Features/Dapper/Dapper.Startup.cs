using MPTDotNetCore.Shared.Services;

namespace MPTDotNetCore.ConsoleApp.Features.Dapper;

public static class Dapper
{

    #region Static Startup (Program.cs )

    public static void ExecuteDapper()
    {
        DbService dbService = new();
        string connection = dbService.GetConnection();

        DapperExample dapperExample = new DapperExample();
    }

    #endregion
}