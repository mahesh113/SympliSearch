using Moq.Protected;
using System.Net;
using SympliDevelopment.Api.Service;
using Microsoft.Extensions.Options;
using SympliDevelopment.Api.Models;

namespace SympliAPI.Tests
{
    public class GoogleSearchServiceTests
    {
        private GoogleSearch _sut; 
        private IFixture fixture;

        private void Setup()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            var mockFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'name':thecodebuzz,'city':'USA'}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            HttpHandler httpHandler = new HttpHandler(client);

            fixture.Register(() => httpHandler);
            _sut = fixture.Create<GoogleSearch>();
            //var configFake = new GConfig();
            //_sut = new GoogleSearch(configFake,httpHandler);
        }
        [Fact]
        public async Task TestSearch()
        {
            Setup();

            var ret = await _sut.Search("https://www.mysite.com.au", "australia");
            Assert.IsType<GoogleSearch>(ret);
        }
    }


    public class GConfig : IOptions<GoogleSearchConfig>
    {
        public GoogleSearchConfig Value
        {
            get
            {
                return new GoogleSearchConfig(); // TODO: Add your settings for test here.
            }
        }
    }
}