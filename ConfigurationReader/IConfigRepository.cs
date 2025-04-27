using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationReaderLib;

public interface IConfigRepository
{
    Task<List<ConfigurationItem>> GetActiveConfigurationsAsync(string applicationName);
}
