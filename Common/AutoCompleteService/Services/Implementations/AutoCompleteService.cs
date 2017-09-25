using System.Collections.Generic;
using AutoCompleteService.Models.DataTransfer;
using System.Linq;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace AutoCompleteService.Services.Implementations
{
    public abstract class AutoCompleteService : IAutoCompleteService
    {
        public async Task<AutoCompleteModel> GetAutoCompleteList(string inputValue)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = new AutoCompleteModel();

            result.List = await FindAutoCompleteList(inputValue);

            stopwatch.Stop();
            result.ResponseTime = stopwatch.ElapsedMilliseconds;

            return result;
        }

        public virtual Task InitData(IDataProviderService dataProvider)
        {
            throw new NotImplementedException();
        }

        protected virtual Task<string[]> FindAutoCompleteList(string value)
        {
            throw new NotImplementedException();
        }

        
    }
}
