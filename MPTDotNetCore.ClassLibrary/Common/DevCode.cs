using Newtonsoft.Json;

namespace MPTDotNetCore.Shared.Common;

public static class DevCode
{
    #region To Dictionary

    public static Dictionary<string, object> ToDictionary<T>(this T obj)
    {
        var keyValue = new Dictionary<string, object>();
        var type = typeof(T);
        foreach (var property in type.GetProperties())
        {
            var value = property.GetValue(obj);
            keyValue.Add($"@{property.Name}", value!);
        }
        return keyValue;
    }

    #endregion

    #region To Json

    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    #endregion

    #region To Object

    public static T ToObject<T>(this string str)
    {
        return JsonConvert.DeserializeObject<T>(str)!;
    }

    public static List<T> ToObjectLst<T>(this string str)
    {
        return JsonConvert.DeserializeObject<List<T>>(str)!;
    }

    #endregion
}