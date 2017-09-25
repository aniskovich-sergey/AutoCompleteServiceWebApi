using AutoCompleteService.Models.DataTransfer;
using System.Threading.Tasks;

namespace AutoCompleteService.Services
{
    /// <summary>
    /// Сервис для поиска вариантов автодополнения
    /// </summary>
    public interface IAutoCompleteService
    {
        /// <summary>
        /// Поиск подходящих строк для автодополнения
        /// </summary>
        /// <param name="inputValue">Введенное пользователем значение</param>
        /// <returns>Модель для контрола с автодополнением</returns>
        Task<AutoCompleteModel> GetAutoCompleteList(string inputValue);

        /// <summary>
        /// Инициализация словаря
        /// </summary>
        /// <param name="dataProvider">Провайдер данных для загрузки словаря</param>
        Task InitData(IDataProviderService dataProvider);
    }
}
