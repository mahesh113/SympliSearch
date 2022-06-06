using Moq.Protected;
using System.Net;
using SympliDevelopment.Api.Service;
using Microsoft.Extensions.Options;
using SympliDevelopment.Api.Models;
using Newtonsoft;
using Newtonsoft.Json;
namespace SympliAPI.Tests
{
    public class GoogleSearchServiceTests
    {
        private GoogleSearch _sut; 
        private IFixture fixture;
        private Mock<IHttpClientFactory> mockFactory; // declared outside to verify the http calls in test cases.
        private GSResponse httpResponse = new GSResponse();
        private Mock<HttpMessageHandler> mockHttpMessageHandler;
        private GoogleSearchConfig _config = new GoogleSearchConfig
        {
            Key = "dummyKey",
            Name = "Google",
            SearchEngineId = "abc12345",
            Url = "http://dummyUrl.com.au"
        };

        private void Setup()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            mockFactory = fixture.Freeze<Mock<IHttpClientFactory>>();
            httpResponse = fixture.Create<GSResponse>();
            mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(httpResponse)),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            HttpHandler httpHandler = new HttpHandler(client);

            fixture.Register(() => httpHandler);

            var optionGSConfig = fixture.Freeze<Mock<IOptions<GoogleSearchConfig>>>();
            optionGSConfig.Setup(x => x.Value).Returns(_config);
            _sut = fixture.Create<GoogleSearch>();
            
        }
        [Fact]
        public async Task TestSearchWithAllDummyData()
        {
            Setup();
            var ret = await _sut.Search("https://www.mysite.com.au", "australia");

            Assert.NotNull(ret);
            Assert.IsType<string>(ret);
        }
        [Fact]
        public async Task CodeTryingToAccessPageWithNoResult()
        {
            Setup();
            httpResponse = fixture.Create<GSResponse>();
            httpResponse.Items.Clear(); // No Items here

            mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(httpResponse)),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            HttpHandler httpHandler = new HttpHandler(client);

            fixture.Register(() => httpHandler);
            var ret = await _sut.Search("https://www.mysite.com.au", "australia");

            Assert.NotNull(ret);
            Assert.IsType<string>(ret);
            Assert.Equal("0",ret);
        }

        [Fact]
        public async Task SearchResultsFindsTheUrlInResults()
        {
            string url = "https://www.mysite.com.au";
            //Setup();
            fixture = new Fixture() { RepeatCount = 10 }.Customize(new AutoMoqCustomization()); // create 10 items on each create
            mockFactory = fixture.Freeze<Mock<IHttpClientFactory>>();
            httpResponse = fixture.Create<GSResponse>(); // same response object returned on every page.
            httpResponse.Items.First().Link = url; // so url inserted on first item of every page

            mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(httpResponse)),
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            HttpHandler httpHandler = new HttpHandler(client);

            fixture.Register(() => httpHandler);
            var optionGSConfig = fixture.Freeze<Mock<IOptions<GoogleSearchConfig>>>();
            optionGSConfig.Setup(x => x.Value).Returns(_config);
            _sut = fixture.Create<GoogleSearch>();

            var ret = await _sut.Search(url, "australia");

            Assert.NotNull(ret);
            Assert.IsType<string>(ret);
            Assert.Equal("1,11,21,31,41,51,61,71,81,91", ret);
        }
    }
}