using AutoCompleteService.Native.Global;
using Microsoft.AspNetCore.Mvc;
using AutoCompleteService.Services;
using AutoCompleteService.Models.DataTransfer;
using Autofac;
using System.Threading.Tasks;
using System;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoCompleteService.Native.Controllers
{
    [Route("api/[controller]")]
    public class AutoCompleteController : Controller
    {
        // менеджеры поиска строк для автодополнения
        private readonly IAutoCompleteService _autoCompleteManagerLine;
        private readonly IAutoCompleteService _autoComplateManagerBinarySearch;
        private readonly IAutoCompleteService _autoCompleteManagerTree;

        // провайдер данных для менеджера поиска строк для автодополнения
        private readonly IDataProviderService _dataProvider;

        public AutoCompleteController(IServiceProvider serviceProvider)
        {
            // инициализация менеджеров
            _autoCompleteManagerLine = IoC.Container.ResolveNamed<IAutoCompleteService>("LineSearch");
            _autoComplateManagerBinarySearch = IoC.Container.ResolveNamed<IAutoCompleteService>("BinarySearch");
            _autoCompleteManagerTree = IoC.Container.ResolveNamed<IAutoCompleteService>("TreeSearch");

            //_dataProvider = IoC.Container.Resolve<IDataProviderService>();
            _dataProvider = (IDataProviderService)serviceProvider.GetService(typeof(IDataProviderService));

            // Инициализация словарей
            InitData();
        }

        /// <summary>
        /// Get запрос с линейным поиском
        /// </summary>
        /// <param name="value">Введенное значение</param>
        /// <returns>Строки для автодополнения</returns>
        [Route("line/{value}")]
        [HttpGet]
        public async Task<AutoCompleteModel> GetLineFind(string value)
        {
            return await _autoCompleteManagerLine.GetAutoCompleteList(value);
        }

        /// <summary>
        /// Get запрос с бинарным поиском
        /// </summary>
        /// <param name="value">Введенное значение</param>
        /// <returns>Строки для автодополнения</returns>
        [Route("binary/{value}")]
        [HttpGet]
        public async Task<AutoCompleteModel> GetBinaryFind(string value)
        {
            return await _autoComplateManagerBinarySearch.GetAutoCompleteList(value);
        }

        /// <summary>
        /// Get запрос с поиском по дереву
        /// </summary>
        /// <param name="value">Введенное значение</param>
        /// <returns>Строки для автодополнения</returns>
        [Route("tree/{value}")]
        [HttpGet]
        public async Task<AutoCompleteModel> GetTreeFind(string value)
        {
            return await _autoCompleteManagerTree.GetAutoCompleteList(value);
        }

        [Route("refresh")]
        [HttpPost]
        public async Task<string> RefreshDictionary()
        {
            await InitData();
            return "OK";
        }

        private Task InitData()
        {
            return Task.Run(() =>
            {
                _autoCompleteManagerLine.InitData(_dataProvider);
                _autoComplateManagerBinarySearch.InitData(_dataProvider);
                _autoCompleteManagerTree.InitData(_dataProvider);
            });
        }
    }
}
