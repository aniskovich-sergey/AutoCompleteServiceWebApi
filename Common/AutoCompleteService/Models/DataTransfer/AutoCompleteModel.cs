namespace AutoCompleteService.Models.DataTransfer
{
    /// <summary>
    /// Модель для контрола с автодополнением
    /// </summary>
    public class AutoCompleteModel
    {
        /// <summary>
        /// Список предложенных строк для ввода
        /// </summary>
        public string[] List { get; set; }

        /// <summary>
        /// Время обработки запроса на сервере, мс
        /// </summary>
        public long ResponseTime { get; set; }
    }
}
