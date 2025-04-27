using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConfigurationReaderLib;

public class ConfigurationCache
{
    private readonly ConcurrentDictionary<string, ConfigurationItem> _cache = new();

    public void UpdateCache(IEnumerable<ConfigurationItem> items)
    {
        foreach (var item in items)
        {
            _cache[item.Name] = item;
        }
    }

    public bool TryGet(string key, out ConfigurationItem item)
    {
        return _cache.TryGetValue(key, out item);
    }

    public IReadOnlyDictionary<string, ConfigurationItem> Snapshot => _cache;
}
