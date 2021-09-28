using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using UrlShortener.Services.Data.Helpers;
using UrlShortener.Controllers;
using UrlShortener.Services.Data.DatabaseModels;
using UrlShortener.Services.Data.Repositories;

namespace UrlShortenerUnitTestProject
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<IShortenedUrlRepository> _shortenedUrlRepositoryMock;

        [SetUp]
        public void Setup()
        {
            Mock<ILogger<HomeController>> loggerMock = new Mock<ILogger<HomeController>>();
            Mock<IRepositoryAccessHandler> repositoryAccessHandlerMock = new Mock<IRepositoryAccessHandler>();
            _shortenedUrlRepositoryMock = new Mock<IShortenedUrlRepository>();

            repositoryAccessHandlerMock.Setup(x => x.AccessShortenedUrls()).Returns(_shortenedUrlRepositoryMock.Object);

            _controller = new HomeController(loggerMock.Object, repositoryAccessHandlerMock.Object)
            {
                ControllerContext = new ControllerContext()
            };

            _controller.ControllerContext.HttpContext = new DefaultHttpContext();            
        }

        [Test]
        public void Index_No_Token_Test()
        {
            _controller.Index("");
            Assert.Pass();
        }

        [Test]
        public void Index_Token_Test()
        {
            _shortenedUrlRepositoryMock.Setup(x => x.Find(1031)).Returns(new ShortenedUrl { OriginalUrl = "https://www.google.com" });
            var result = _controller.Index("BwQAAA");       
            Assert.IsInstanceOf<RedirectResult>(result);
        }

        [TestCase("T.me")]
        [TestCase("google.com")]
        [TestCase("www.google.com")]
        [TestCase("http://www.google.com")]
        [TestCase("https://www.google.com")]
        [TestCase("https://www.instagram.com/cats_of_instagram/?hl=en")]
        [TestCase("https://www.cagesideseats.com/wwe/2021/9/24/22691055/wwe-smackdown-results-live-blog-sept-24-2021-extreme-rules-go-home-nakamura-crews-corbin-happy-talk")]
        [TestCase("https://www.google.co.uk/maps/place/The+Thirsty+Goat+Belfast/@54.6013568,-5.9285364,17z/data=!4m12!1m6!3m5!1s0x486108540c517487:0x97fb48b30800939b!2sThe+Thirsty+Goat+Belfast!8m2!3d54.6013568!4d-5.9263477!3m4!1s0x486108540c517487:0x97fb48b30800939b!8m2!3d54.6013568!4d-5.9263477")]
        public void Index_ShortenUrl_Test(string testUrls)
        {
            _shortenedUrlRepositoryMock.Setup(x => x.InsertOrUpdate(It.IsAny<ShortenedUrl>())).Returns(new ShortenedUrl { ShortenedUrlId = 1 });

            JsonResult result = (JsonResult)_controller.ShortenUrl(testUrls);
            int status = _controller.ControllerContext.HttpContext.Response.StatusCode;

            if(((string)result.Value.GetType().GetProperty("shortenedUrlToken").GetValue(result.Value, null)).Length < 150 && status == 200)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();             
            }
            
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("D:\\test")]
        [TestCase(".com")]
        [TestCase("www.test$$(.com")]
        public void Index_ShortenUrl_InvalidUrls_Test(string testUrls)
        {
            _shortenedUrlRepositoryMock.Setup(x => x.InsertOrUpdate(It.IsAny<ShortenedUrl>())).Returns(new ShortenedUrl { ShortenedUrlId = 1 });

            _controller.ShortenUrl(testUrls);
            int status = _controller.ControllerContext.HttpContext.Response.StatusCode;

            if (status == 200)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }
    }
}