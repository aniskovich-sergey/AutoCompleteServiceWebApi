using AutoCompleteService.Models.Domain;
using AutoCompleteService.Modules.TreeSearch.Helpers;
using AutoCompleteService.Tests.TestData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoCompleteService.Tests.AutoCompleteService.Modules.TreeSearch
{
    public class TreeHelperTests
    {
        [Fact]
        public void BuildTreeTest()
        {
            // Arrange

            // Act
            var result = TreeHelper.BuildTree(TestDataForSearch.SortedDictionary.ToArray());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(GetExpectedTree(), result, new TreeComparer());
        }

        [Fact]
        public void FindParentForNodeTest()
        {
            // Arrange

            // Act
            var result = TreeHelper.FindParentForNode(new Node("aab"), GetExpectedTree().Root);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("aa", result.Word);
        }

        [Fact]
        public void GetAllNodesTest()
        {
            // Arrange
            var expectedList = new List<string>()
            {
                "aabc",
                "aabcd",
                "aabce"
            };

            // Act
            // Соберем поддерево "aa"
            var result = TreeHelper.GetAllChildNodes(GetExpectedTree().Root.Child[0].Child[0]).Select(x => x.Word).ToArray();

            Assert.NotNull(result);
            Assert.Equal(expectedList, result);
        }

        private Tree GetExpectedTree()
        {
            var result = new Tree();
            result.Root = new Node();
            result.Root.Child.AddRange(new List<Node>()
            {
                new Node()
                {
                    Word = "a", Index = 2,
                    Child = new List<Node>()
                    {
                        new Node()
                        {
                            Word = "aa", Index = 3,
                            Child = new List<Node>()
                            {
                                new Node()
                                {
                                    Word = "aabc", Index = 1,
                                    Child = new List<Node>()
                                    {
                                        new Node()
                                        {
                                            Word = "aabcd", Index = 5,
                                            Child = new List<Node>()
                                        },
                                        new Node()
                                        {
                                            Word = "aabce", Index = 2,
                                            Child = new List<Node>()
                                        }
                                    }
                                }
                            }
                        },
                        new Node()
                        {
                            Word = "ab", Index = 4,
                            Child = new List<Node>()
                            {
                                new Node()
                                {
                                    Word = "abc", Index = 1,
                                    Child = new List<Node>()
                                }
                            }
                        },
                        new Node()
                        {
                            Word = "ac", Index = 6,
                            Child = new List<Node>()
                        }
                    }
                },
                new Node()
                {
                    Word = "b", Index = 4,
                    Child = new List<Node>()
                    {
                        new Node()
                        {
                            Word = "ba", Index = 2,
                            Child = new List<Node>()
                        },
                        new Node()
                        {
                            Word = "bc", Index = 2,
                            Child = new List<Node>()
                        }
                    },
                },
                new Node()
                {
                    Word = "dca", Index = 1,
                    Child = new List<Node>()
                },
            });
            return result; ;
        }

    }

    internal class TreeComparer : IEqualityComparer<Tree>
    {
        public bool Equals(Tree x, Tree y)
        {
            return NodeEqual(x.Root, y.Root);
        }

        private bool NodeEqual(Node x, Node y)
        {
            if (x.Word != y.Word || x.Index != y.Index)
                return false;
            if (x.Child.Count != y.Child.Count)
                return false;
            for (int i = 0; i < x.Child.Count; i++)
            {
                if (!NodeEqual(x.Child[i], y.Child[i]))
                    return false;
            }
            return true;
        }

        public int GetHashCode(Tree obj)
        {
            return obj.GetHashCode();
        }
    }
}
