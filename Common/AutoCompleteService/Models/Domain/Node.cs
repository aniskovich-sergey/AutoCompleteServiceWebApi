using System.Collections.Generic;

namespace AutoCompleteService.Models.Domain
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Слово
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Частота встречания
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Node> Child { get; set; }

        #region .ctors

        public Node(KeyValuePair<string, int> item)
        {
            Word = item.Key;
            Index = item.Value;
            Child = new List<Node>();
        }

        public Node()
        {
            Child = new List<Node>();
        }

        public Node(string value)
        {
            Child = new List<Node>();
            Word = value;
            Index = -1;
        }

        #endregion .ctors

    }
}
