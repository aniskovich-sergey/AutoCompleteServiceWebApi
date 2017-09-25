using AutoCompleteService.Modules.FileDataProvider.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AutoCompleteService.Tests.AutoCompleteService.Modules.FileDataProvider
{
    public class FileDataProviderTests
    {
        [Fact]
        public void GetDataNotNull()
        {
            // Arange
            var testFileName = "TestDataFileDataProviderService.txt";
            var path = Path.Combine(AppContext.BaseDirectory, "TestData", testFileName);
            var provider = new FileDataProviderService(path);

            // Act
            var result = provider.GetData();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetData()
        {
            // Arange
            var testFileName = "TestDataFileDataProviderService.txt";
            var path = Path.Combine(AppContext.BaseDirectory, "TestData", testFileName);
            var provider = new FileDataProviderService(path);

            var expected = new SortedDictionary<string, int>()
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

            // Act
            var result = provider.GetData();
            

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
