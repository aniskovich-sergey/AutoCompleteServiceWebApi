using AutoCompleteService.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementations = AutoCompleteService.Services.Implementations;


namespace AutoCompleteService.Modules.LineSearch.Services
{
    public class LineSearchAutoCompleteService : Implementations.AutoCompleteService
    {
        private Dictionary<string, int> dictionary;

        public override Task InitData(IDataProviderService dataProvider)
        {
            return Task.Run(() =>
            {
                dictionary = dictionary ?? new Dictionary<string, int>(dataProvider.GetData());
            });
        }

        protected override Task<string[]> FindAutoCompleteList(string value)
        {
            return Task.Run(() =>
            {
                value = value.ToLower();
                return dictionary?
                    .Where(x => x.Key.ToLower().StartsWith(value))
                    .OrderByDescending(b => b.Value)
                    .Take(10)
                    .Select(a => a.Key)
                    .ToArray();
            });
        }
    }
}
