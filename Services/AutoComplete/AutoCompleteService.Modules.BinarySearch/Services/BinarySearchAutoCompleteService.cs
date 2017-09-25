using System.Collections.Generic;
using System.Linq;
using AutoCompleteService.Modules.BinarySearch.Helpers;
using Implementations = AutoCompleteService.Services.Implementations;
using AutoCompleteService.Services;
using System.Threading.Tasks;

namespace AutoCompleteService.Modules.BinarySearch.Services
{
    public class BinarySearchAutoCompleteService : Implementations.AutoCompleteService
    {
        private List<KeyValuePair<string, int>> sortedDictionary;
        protected override Task<string[]> FindAutoCompleteList(string value)
        {
            return Task.Run(() =>
            {
                value = value.ToLower();
                var startWith = GetListStartWith(value);
                return startWith?
                    .OrderByDescending(b => b.Value)
                    .Take(10)
                    .Select(a => a.Key)
                    .ToArray();
            });
        }

        public override Task InitData(IDataProviderService dataProvider)
        {
            return Task.Run(() =>
            {
                sortedDictionary = sortedDictionary ?? dataProvider.GetData().ToList();
            });
                
        }

        private IEnumerable<KeyValuePair<string, int>> GetListStartWith(string value)
        {
            var list = sortedDictionary.ToList();
            int i = list.BinarySearch(new KeyValuePair<string, int>(value, 1), new ComparerKeyPair());
            if (i < 0)
                i = -i - 1;

            //идем вверх до первого  элемента начинающегося с value
            i = i - 1;
            while (i >= 0 && list[i].Key.StartsWith(value))
                i--;
            i++;

            //перебираем все элементы начинающиеся на с value
            while (i < sortedDictionary.Count && list[i].Key.StartsWith(value))
                yield return list[i++];
        }
    }
}
