using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationReaderLib;

public class SqlConfigRepository : IConfigRepository
{
    private readonly string _connectionString;

    public SqlConfigRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<ConfigurationItem>> GetActiveConfigurationsAsync(string applicationName)
    {
        Console.WriteLine($"[DEBUG] SQL'e gönderilen app name: '{applicationName}'"); // 🟢 BURAYA EKLE

        var result = new List<ConfigurationItem>();

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var query = @"SELECT Name, Type, Value FROM Configurations
                  WHERE IsActive = 1 AND ApplicationName = @ApplicationName";

        using var cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@ApplicationName", applicationName);

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new ConfigurationItem
            {
                Name = reader.GetString(0),
                Type = reader.GetString(1),
                Value = reader.GetString(2)
            });
        }

        return result;
    }

}
