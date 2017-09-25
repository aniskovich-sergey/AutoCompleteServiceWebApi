using AutoCompleteService.Modules.TreeSearch.Services;
using AutoCompleteService.Tests.TestData;
using Xunit;

namespace AutoCompleteService.Tests.AutoCompleteService.Modules.TreeSearch
{
    public class TreeSearchTests
    {
        [Fact]
        public async void InitDataInput_a()
        {
            // Arrange
            var search = new TreeSearchAutoCompleteService();

            // Act
            await search.InitData(TestDataForSearch.Mock.Object);
            var result = await search.GetAutoCompleteList("a");

            // Assert
            Assert.Equal(TestDataForSearch.ExpectedInput_a.List, result.List);
        }

        [Fact]
        public async void InitDataInput_b()
        {
            // Arrange
            var search = new TreeSearchAutoCompleteService();

            // Act
            await search.InitData(TestDataForSearch.Mock.Object);
            var result = await search.GetAutoCompleteList("b");

            // Assert
            Assert.Equal(TestDataForSearch.ExpectedInput_b.List, result.List);
        }

        [Fact]
        public async void InitDataInput_c()
        {
            // Arrange
            var search = new TreeSearchAutoCompleteService();

            // Act
            await search.InitData(TestDataForSearch.Mock.Object);
            var result = await search.GetAutoCompleteList("c");

            // Assert
            Assert.Empty(result.List);
        }

        [Fact]
        public async void InitDataInput_aa()
        {
            // Arrange
            var search = new TreeSearchAutoCompleteService();

            // Act
            await search.InitData(TestDataForSearch.Mock.Object);
            var result = await search.GetAutoCompleteList("aa");

            // Assert
            Assert.Equal(TestDataForSearch.ExpectedInput_aa.List, result.List);
        }

        [Fact]
        public async void InitDataInput_aaw()
        {
            // Arrange
            var search = new TreeSearchAutoCompleteService();

            // Act
            await search.InitData(TestDataForSearch.Mock.Object);
            var result = await search.GetAutoCompleteList("aaw");

            // Assert
            Assert.Empty(result.List);
        }
    }
}
