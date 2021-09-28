using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using UrlShortener.Resources;
using UrlShortener.Services.Data.Helpers;
using UrlShortener.Exceptions;
using UrlShortener.Services.Data.DatabaseModels;
using UrlShortener.Services.Data.Repositories;

namespace UrlShortener.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepositoryAccessHandler _repositoryAccessHandler;

        public HomeController(ILogger<HomeController> logger, IRepositoryAccessHandler repositoryAccessHandler) : base(logger)
        {
            _repositoryAccessHandler = repositoryAccessHandler;
        }

        [HttpGet]
        public IActionResult Index(string shortenedUrlToken)
        {
            try
            {
                if (string.IsNullOrEmpty(shortenedUrlToken))
                {
                    return View();
                }
                else
                {
                    using (IShortenedUrlRepository shortenedUrlRepository = _repositoryAccessHandler.AccessShortenedUrls())
                    {
                        string originalUrl = shortenedUrlRepository.Find(ShortenedUrl.GetIdFromUrlToken(shortenedUrlToken)).OriginalUrl;
                        return Redirect(originalUrl);
                    }
                }
            }
            catch (Exception exception)
            {
                return InternalServerErrorResponse(exception, ControllerMessages.Index_InternalServerErrorResponse);
            }

        }

        [HttpPost]
        public IActionResult ShortenUrl(string url)
        {
            try
            {
                ShortenedUrl result;

                using (var shortenedUrlRepository = _repositoryAccessHandler.AccessShortenedUrls())
                {
                    result = shortenedUrlRepository.InsertOrUpdate(new ShortenedUrl { OriginalUrl = GetValidUrl(url).AbsoluteUri });
                }

                return Json(new { shortenedUrlToken = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{result.GetUrlToken()}" });
            }
            catch (InvalidUrlException)
            {
                return BadRequestResponse(ControllerMessages.ShortenUrl_BadRequestResponse);
            }
            catch (Exception exception)
            {
                return InternalServerErrorResponse(exception, ControllerMessages.ShortenUrl_InternalServerErrorResponse);
            }
        }

        private Uri GetValidUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (!url.Contains("http"))
                {
                    url = $"https://{url}";
                }

                bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (result)
                {
                    return uriResult;
                }
            }

            throw new InvalidUrlException("Invalid URL");
        }
    }
}
