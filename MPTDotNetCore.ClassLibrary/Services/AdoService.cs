using System.Data.SqlClient;
using System.Data;
using MPTDotNetCore.Shared.Common;

namespace MPTDotNetCore.Shared.Services;

public class AdoService
{
    #region Constructor

    private string _connection;

    public AdoService(string connection)
    {
        _connection = connection;
    }

    #endregion

    #region Get Methods

    private DataTable Get(string query, Dictionary<string, object>? keyValues = null)
    {
        using (var connection = new SqlConnection(_connection))
        {
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                if (keyValues != null)
                {
                    foreach (var kvp in keyValues)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }
                }

                var dt = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }

                return dt;
            }
        }
    }

    public T GetLst<T>(string query, Dictionary<string, object>? keyValues = null)
    {
        var dt = Get(query, keyValues);
        var jsonStr = dt.ToJson();
        return jsonStr.ToObject<T>();
    }

    public T GetItem<T>(string query, Dictionary<string, object>? keyValues = null)
    {
        var dt = Get(query, keyValues);
        var jsonStr = dt.ToJson();
        var jsonObj = jsonStr.ToObject<List<T>>();
        return jsonObj.FirstOrDefault()!;
    }

    #endregion

    #region Execute Queries

    private int ExecuteQuery(string query, Dictionary<string, object>? keyValues = null)
    {
        using (var connection = new SqlConnection(_connection))
        {
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                if (keyValues != null)
                {
                    foreach (var kvp in keyValues)
                    {
                        cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }
    }

    public int Execute(string query, Dictionary<string, object>? keyValues = null)
    {
        return ExecuteQuery(query, keyValues);
    }

    #endregion

    #region Result Message

    public void ResultMessage<T>(T result)
    {
        Console.WriteLine(result!.ToJson());
    }

    public void ResultMessage(int result, string operation)
    {
        Console.WriteLine(result > 0 ? $"{operation} Successful." : $"{operation} Failed. No rows affected.");
    }

    #endregion
}