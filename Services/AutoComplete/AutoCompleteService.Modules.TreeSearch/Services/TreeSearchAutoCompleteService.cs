using System.Linq;
using AutoCompleteService.Models.Domain;
using AutoCompleteService.Modules.TreeSearch.Helpers;
using Implementations = AutoCompleteService.Services.Implementations;
using AutoCompleteService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoCompleteService.Modules.TreeSearch.Services
{
    public class TreeSearchAutoCompleteService : Implementations.AutoCompleteService
    {
        private Tree _tree;

        public override Task InitData(IDataProviderService dataProvider)
        {
            return Task.Run(() =>
            {
                // строим дерево
                _tree = _tree ?? TreeHelper.BuildTree(dataProvider.GetData().ToArray());
            });
        }

        protected override Task<string[]> FindAutoCompleteList(string value)
        {
            return Task.Run(() =>
            {
                value = value.ToLower();
                // находим узел, который начинается с value
                var rootSubTree = TreeHelper.FindParentForNode(new Node(value), _tree.Root);

                // Если корень поддерева совпадает с корнем дерева
                // значит значение не найдено в словаре
                if (rootSubTree == _tree.Root)
                    return new string[] { };

                var startWith = new List<Node>();

                // Если корень поддерева начинается с value, то его тоже вносим в список
                if (rootSubTree.Word != null && rootSubTree.Word.StartsWith(value))
                    startWith.Add(rootSubTree);

                // собираем все дочерние узлы в кучу
                startWith.AddRange(TreeHelper.GetAllChildNodes(rootSubTree));

                return startWith?
                    .OrderByDescending(b => b.Index)
                    .Take(10)
                    .Select(a => a.Word)
                    .ToArray();
            });
        }
    }
}
