using IndicinaDecideLibrary;
using System.Net;

namespace IndicinaDecideLibraryTests
{
    [TestClass]
    public class DecideAuthTests
    {
        [TestMethod]
        public void GenerateAccessToken_Successful()
        {
            // Arrange
            var mockHttpClientService = new MockHttpClientService();

            var authorization = new DecideAuth("clientId", "clientSecret", mockHttpClientService);

            // Act
            var accessToken = authorization.GetAccessToken();

            // Assert
            Assert.AreEqual("fake_token", accessToken);
        }

        [TestMethod]
        public void GenerateAccessToken_MissingClientId_ThrowsException()
        {
            // Arrange
            var mockHttpClientService = new MockHttpClientService();

            // Act & Assert
            Assert.ThrowsException<DecideException>(() => new DecideAuth("", "clientSecret", mockHttpClientService));
        }

        [TestMethod]
        public void GenerateAccessToken_MissingClientSecret_ThrowsException()
        {
            // Arrange
            var mockHttpClientService = new MockHttpClientService();

            // Act & Assert
            Assert.ThrowsException<DecideException>(() => new DecideAuth("clientId", "", mockHttpClientService));
        }

        [TestMethod]
        public void GenerateAccessToken_FailedResponse_ThrowsException()
        {
            // Arrange
            var mockHttpClientService = new MockHttpClientService(HttpStatusCode.BadRequest);

            // Act & Assert
            Assert.ThrowsException<DecideException>(() => new DecideAuth("clientId", "clientSecret", mockHttpClientService));
        }

        [TestMethod]
        public void GenerateAccessToken_InvalidResponse_ThrowsException()
        {
            // Arrange
            var mockHttpClientService = new MockHttpClientService(HttpStatusCode.OK, "{\"Data\": {}}"); // Invalid response content


            // Act & Assert
            Assert.ThrowsException<DecideException>(() => new DecideAuth("clientId", "clientSecret", mockHttpClientService));
        }
    }
}
