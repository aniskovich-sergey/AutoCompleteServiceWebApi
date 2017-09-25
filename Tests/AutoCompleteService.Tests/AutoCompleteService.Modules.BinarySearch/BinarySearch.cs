using AutoCompleteService.Modules.BinarySearch.Services;
using AutoCompleteService.Tests.TestData;
using Xunit;

namespace AutoCompleteService.Tests.AutoCompleteService.Modules.BinarySearch
{
    public class BinarySearch
    {
        [Fact]
        public async void InitDataInput_a()
        {
            // Arrange
            var search = new BinarySearchAutoCompleteService();

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
            var search = new BinarySearchAutoCompleteService();

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
            var search = new BinarySearchAutoCompleteService();

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
            var search = new BinarySearchAutoCompleteService();

            // Act
            await search.InitData(TestDataForSearch.Mock.Object);
            var result = await search.GetAutoCompleteList("aa");

            // Assert
            Assert.Equal(TestDataForSearch.ExpectedInput_aa.List, result.List);
        }
    }
}
