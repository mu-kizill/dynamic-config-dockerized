using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReaderLib;

public class ConfigurationReader
{
    private readonly string _applicationName;
    private readonly int _refreshIntervalMs;
    private readonly ConfigurationCache _cache = new();
    private readonly IConfigRepository _repository;
    private Timer _timer;

    public ConfigurationReader(string applicationName, string connectionString, int refreshIntervalInMs)
    {
        _applicationName = applicationName;
        _refreshIntervalMs = refreshIntervalInMs;
        _repository = new SqlConfigRepository(connectionString);

        LoadConfigurationsAsync().Wait();

        _timer = new Timer(async _ => await RefreshCacheAsync(), null, _refreshIntervalMs, _refreshIntervalMs);
    }

    private async Task LoadConfigurationsAsync()
    {
        try
        {
            var configs = await _repository.GetActiveConfigurationsAsync(_applicationName);
            _cache.UpdateCache(configs);
        }
        catch
        {
            // Hata durumunda cache'deki mevcut veriyi koruyoruz
        }
    }

    private async Task RefreshCacheAsync()
    {
        await LoadConfigurationsAsync();
    }

    public T GetValue<T>(string key)
    {
        if (_cache.TryGet(key, out var item))
        {
            try
            {
                return (T)Convert.ChangeType(item.Value, typeof(T));
            }
            catch
            {
                throw new InvalidCastException($"Anahtar '{key}' için tür dönüştürmesi başarısız: {item.Type} → {typeof(T).Name}");
            }
        }

        throw new KeyNotFoundException($"'{key}' anahtarı için aktif bir konfigürasyon bulunamadı.");
    }
}
