using AutoCompleteService.Models.DataTransfer;
using AutoCompleteService.Services;
using Moq;
using System.Collections.Generic;

namespace AutoCompleteService.Tests.TestData
{
    public static class TestDataForSearch
    {
        private static Mock<IDataProviderService> _mock;

        public static Mock<IDataProviderService> Mock
        {
            get
            {
                if (_mock == null)
                {
                    _mock = new Mock<IDataProviderService>();
                    _mock.Setup(provider => provider.GetData()).Returns(TestDataForSearch.SortedDictionary);
                }
                return _mock;
            }
        }

        public static SortedDictionary<string, int> SortedDictionary
        {
            get
            {
                return new SortedDictionary<string, int>()
                    {
                        { "a",      2 },
                        { "aa",     3 },
                        { "aabc",   1 },
                        { "aabcd",  5 },
                        { "aabce",  2 },
                        { "ab",     4 },
                        { "abc",    1 },
                        { "ac",     6 },
                        { "b",      4 },
                        { "ba",     2 },
                        { "bc",     2 },
                        { "dca",    1 },
                    };
            }
        }

        public static AutoCompleteModel ExpectedInput_a
        {
            get
            {
                return new AutoCompleteModel
                {
                    List = new string[]
                   {
                       "ac", "aabcd", "ab", "aa", "a", "aabce", "aabc", "abc"
                   }
                };
            }
        }

        public static AutoCompleteModel ExpectedInput_b
        {
            get
            {
                return new AutoCompleteModel
                {
                    List = new string[]
                   {
                       "b", "ba", "bc"
                   }
                };
            }
        }

        public static AutoCompleteModel ExpectedInput_aa
        {
            get
            {
                return new AutoCompleteModel
                {
                    List = new string[]
                   {
                       "aabcd", "aa", "aabce", "aabc"
                   }
                };
            }
        }
    }
}
