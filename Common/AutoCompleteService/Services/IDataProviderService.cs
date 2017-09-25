using System.Collections.Generic;

namespace AutoCompleteService.Services
{
    /// <summary>
    /// Загрузчик данных данных
    /// </summary>
    public interface IDataProviderService
    {
        /// <summary>
        /// Загрузка данных для автодополнения
        /// </summary>
        /// <returns>Отсортированный словарь</returns>
        IDictionary<string, int> GetData();
    }
}
