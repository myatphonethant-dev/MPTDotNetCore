﻿using System.Data.SqlClient;

namespace MPTDotNetCore.Shared.Services;

public class DbService
{
    public string GetConnection()
    {
        var connection = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };

        return connection.ConnectionString;
    }
}