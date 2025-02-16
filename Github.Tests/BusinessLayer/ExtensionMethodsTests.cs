using System;
using Github.BusinessLayer.Extensions;


namespace Github.Tests.BusinessLayer
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void TestStringTrimWithSimpleList()
        {
            // Arrange
            var items = new List<string>
            {
                "christian.graus@gmail.com",
                "fred.smith@outlook.com",
                "billybob@hotmail.com"
            };

            // Act
            items = items.RemoveDuplicates();

            // Assert
            Assert.AreEqual(items.Count(), 3);
        }

        [TestMethod]
        public void TestStringTrimWithDuplicateList()
        {
            // Arrange
            var items = new List<string>
            {
                "christian.graus@gmail.com",
                "fred.smith@outlook.com",
                "christian.graus@gmail.com",
                "billybob@hotmail.com"
            };

            // Act
            items = items.RemoveDuplicates();

            Assert.AreEqual(items.Count(), 3);
            var christians = items.Where(e => e == "christian.graus@gmail.com").ToList();

            // Assert
            Assert.AreEqual(christians.Count(), 1);
        }

        [TestMethod]
        public void TestStringTrimWithDuplicateListCaseVariation()
        {
            // Arrange
            var items = new List<string>
            {
                "christian.graus@gmail.com",
                "fred.smith@outlook.com",
                "Christian.Graus@gmail.com",
                "billybob@hotmail.com"
            };

            // Act
            items = items.RemoveDuplicates();

            // Assert
            Assert.AreEqual(items.Count(), 3);
            var christians = items.Where(e => e == "christian.graus@gmail.com").ToList();

            Assert.AreEqual(christians.Count(), 1);
        }

        [TestMethod]
        public void TestStringTrimWithDuplicateListCaseVariationOn()
        {
            // Arrange
            var items = new List<string>
            {
                "christian.graus@gmail.com",
                "fred.smith@outlook.com",
                "Christian.Graus@gmail.com",
                "billybob@hotmail.com"
            };

            // Act
            items = items.RemoveDuplicates(false);

            // Assert
            Assert.AreEqual(items.Count(), 4);
            var christians = items.Where(e => string.Equals(e, "christian.graus@gmail.com", StringComparison.OrdinalIgnoreCase)).ToList();

            Assert.AreEqual(christians.Count(), 2);
        }

        [TestMethod]
        public void TestStringTrimWithDuplicateListWithSpaces()
        {
            // Arrange
            var items = new List<string>
            {
                "christian.graus@gmail.com",
                "fred.smith@outlook.com",
                "   christian.graus@gmail.com",
                "christian.graus@gmail.com   ",
                "billybob@hotmail.com"
            };

            // Act
            items = items.RemoveDuplicates();

            // Assert
            Assert.AreEqual(items.Count(), 3);
            var christians = items.Where(e => e == "christian.graus@gmail.com").ToList();

            Assert.AreEqual(christians.Count(), 1);
        }

        [TestMethod]
        public void TestStringTrimWithDuplicateListRespectSpaces()
        {
            // Arrange
            var items = new List<string>
            {
                "christian.graus@gmail.com",
                "fred.smith@outlook.com",
                "   christian.graus@gmail.com",
                "christian.graus@gmail.com   ",
                "billybob@hotmail.com"
            };

            // Act
            items = items.RemoveDuplicates(stripExtraSpace: false);

            // Assert
            Assert.AreEqual(items.Count(), 5);
            var christians = items.Where(e => e.Trim() == "christian.graus@gmail.com").ToList();

            Assert.AreEqual(christians.Count(), 3);
        }
    }
}
