using System.Collections.Generic;
using System.IO;
using AutoCompleteService.Services;
using System.Linq;

namespace AutoCompleteService.Modules.FileDataProvider.Services
{
    /// <summary>
    /// Сервис загрузки данных из файла
    /// </summary>
    public class FileDataProviderService : IDataProviderService
    {
        private string fileName = "test.in.txt";

        #region .ctors
        /// <summary>
        /// Общий конструктор
        /// </summary>
        public FileDataProviderService()
        {

        }

        /// <summary>
        /// Конструтор для тестов
        /// </summary>
        /// <param name="fileName">Имя файла с тестовыми данными</param>
        public FileDataProviderService(string fileName)
        {
            this.fileName = fileName;
        }

        #endregion .ctors

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <returns>отсортированный словарь</returns>
        public IDictionary<string, int> GetData()
        {
            var dictionary = new SortedDictionary<string, int>();

            if (!File.Exists(fileName))
                return dictionary;

            lock (fileName)
            {
                using (var fileStream = File.Open(fileName, FileMode.Open))
                using (var streamReader = new StreamReader(fileStream))
                {

                    // загрузка словаря
                    int dictionarySize = int.Parse(streamReader.ReadLine());

                    for (int i = 0; i < dictionarySize; i++)
                    {
                        var line = streamReader.ReadLine().Split(' ');
                        var word = line[0];
                        int index = int.Parse(line[1]);
                        dictionary.Add(word, index);
                    }

                    // загрузка истории ввода
                    if (!int.TryParse(streamReader.ReadLine(), out int historySize)) return dictionary;

                    var history = new Dictionary<string, int>();

                    for (int i = 0; i < historySize; i++)
                    {
                        var line = streamReader.ReadLine();
                        if (history.ContainsKey(line))
                        {
                            history[line]++;
                        }
                        else
                        {
                            history.Add(line, 1);
                        }
                    }

                    // объединение двух словарей
                    foreach (var curr in history)
                    {
                        if (dictionary.ContainsKey(curr.Key))
                        {
                            dictionary[curr.Key] += curr.Value;
                        }
                        else
                        {
                            dictionary.Add(curr.Key, curr.Value);
                        }
                    }

                    return dictionary;
                }
            }
        }
    }
}
