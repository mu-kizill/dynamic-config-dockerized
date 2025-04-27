using ConfigurationReaderLib;

var reader = new DynamicConfigurationManager(
    applicationName: "SERVICE-B",
    connectionString: "Server=mssql;Database=DynamicConfigDb;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True;",
    refreshIntervalInMs: 2000
);

Console.WriteLine("[DEBUG] Konfigürasyonlar yükleniyor...");
await Task.Delay(2000);

var siteName = reader.GetValue<string>("ApiBaseUrl");
Console.WriteLine("ApiBaseUrl: " + siteName);

try
{
    var apiUrl = reader.GetValue<string>("ApiBaseUrl");
    Console.WriteLine("ApiBaseUrl: " + apiUrl);
}
catch (Exception ex)
{
    Console.WriteLine("[ERROR] " + ex.Message);
}
