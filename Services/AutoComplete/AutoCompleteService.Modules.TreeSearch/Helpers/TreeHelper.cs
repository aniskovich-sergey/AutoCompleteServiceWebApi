using System.Collections.Generic;
using AutoCompleteService.Models.Domain;


namespace AutoCompleteService.Modules.TreeSearch.Helpers
{
    /// <summary>
    /// Помощник по работе с деревом
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// Составление дерева из сортированного словаря
        /// </summary>
        /// <param name="sortedDictionary">Отсортированный словарь</param>
        /// <returns></returns>
        public static Tree BuildTree(KeyValuePair<string, int>[] sortedDictionary)
        {
            var _tree = new Tree { Root = new Node() };

            for (int i = 0; i < sortedDictionary.Length; i++)
            {
                var node = new Node(sortedDictionary[i]);
                var parNode = FindParentForNode(node, _tree.Root);

                parNode.Child.Add(node);
            }
            return _tree;
        }

        public static Node FindParentForNode(Node node, Node currNode)
        {
            var result = currNode;
            // просматриваем всех детей,
            // если слово у node начинается со слова в текущем ребенке
            // то ползем глубже
            // Не используем LINQ, т.к. это начительно замедляет рекурсию
            foreach (var childNode in currNode.Child)
            {
                if (node.Word.StartsWith(childNode.Word))
                {
                    result = FindParentForNode(node, childNode);
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Собрать дерево в список
        /// </summary>
        /// <param name="rootNode">Корень дерева</param>
        /// <returns></returns>
        public static List<Node> GetAllChildNodes(Node rootNode)
        {
            var result = new List<Node>();
            foreach (var child in rootNode.Child)
            {
                result.Add(child);
                if (child.Child.Count > 0)
                    result.AddRange(GetAllChildNodes(child));
            }

            return result;
        }
    }
}
