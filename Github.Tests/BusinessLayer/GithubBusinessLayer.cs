using Github.BusinessLayer;
using Moq;

namespace Github.Tests.BusinessLayer
{
    [TestClass]
    public class GithubBusinessLayerTests
    {
        [TestMethod]
        public async Task TestBasicCall()
        {
            // Arrange
            var client = new HttpClient();
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            
            var bl = new GithubBusinessLayer(factory);

            var items = new List<string>
            {
                "christiangraus1",
            };

            // Act

            var result = await bl.GetUsers(items);


            // Assert
            Assert.AreEqual(result.Count, 1);

            var user = result.First();

            Assert.AreEqual(user.Login, "christiangraus1");
            Assert.AreNotEqual(user.NumberOfRepositories, 0);
        }

        [TestMethod]
        public async Task TestMany()
        {
            // Arrange
            var client = new HttpClient();
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            var bl = new GithubBusinessLayer(factory);

            var items = new List<string>
            {
                "christiangraus1",
                "turbo124",
                "paulwer"
            };

            // Act

            var result = await bl.GetUsers(items);


            // Assert
            Assert.AreEqual(result.Count, 3);

            var user = result.Where(e => e.Login == "paulwer").FirstOrDefault();

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Login, "paulwer");
            // This is public data so assume these numbers won't go down so the tests don't break
            Assert.IsTrue(user.NumberOfRepositories >= 28);
            Assert.IsTrue(user.NumberOfFollowers >= 8);
        }

        [TestMethod]
        public async Task TestWithDuplicates()
        {
            // Arrange
            var client = new HttpClient();
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            var bl = new GithubBusinessLayer(factory);

            var items = new List<string>
            {
                "christiangraus1",
                "turbo124",
                "christiangraus1",
                "turbo124",
                "paulwer"
            };

            // Act

            var result = await bl.GetUsers(items);


            // Assert
            Assert.AreEqual(result.Count, 3);

            var user = result.Where(e => e.Login == "paulwer").FirstOrDefault();

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Login, "paulwer");
            // This is public data so assume these numbers won't go down so the tests don't break
            Assert.IsTrue(user.NumberOfRepositories >= 28);
            Assert.IsTrue(user.NumberOfFollowers >= 8);
        }

        [TestMethod]
        public async Task TestWithBadData()
        {
            // Arrange
            var client = new HttpClient();
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;

            var bl = new GithubBusinessLayer(factory);

            var items = new List<string>
            {
                "christiangraus1",
                "turbo124",
                "yourmum324988",  // Your mum and astroboy were being used :)
                "astroboy1267",
                "paulwer"
            };

            // Act

            var result = await bl.GetUsers(items);


            // Assert
            Assert.AreEqual(result.Count, 3);

            var user = result.Where(e => e.Login == "paulwer").FirstOrDefault();

            Assert.IsNotNull(user);
            Assert.AreEqual(user.Login, "paulwer");
            // This is public data so assume these numbers won't go down so the tests don't break
            Assert.IsTrue(user.NumberOfRepositories >= 28);
            Assert.IsTrue(user.NumberOfFollowers >= 8);
        }
    }
}
