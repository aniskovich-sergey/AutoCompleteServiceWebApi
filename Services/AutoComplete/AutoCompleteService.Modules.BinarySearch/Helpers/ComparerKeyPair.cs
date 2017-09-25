using System;
using System.Collections.Generic;

namespace AutoCompleteService.Modules.BinarySearch.Helpers
{
    internal class ComparerKeyPair : IComparer<KeyValuePair<String, int>>
    {
        public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            return String.Compare(x.Key, y.Key, StringComparison.Ordinal);
        }
    }
}
