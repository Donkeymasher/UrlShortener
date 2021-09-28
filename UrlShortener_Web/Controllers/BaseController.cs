using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace UrlShortener.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult BadRequestResponse(string responeMessage)
        {
            Response.StatusCode = 400;
            return Json(new { response = responeMessage });
        }

        public IActionResult InternalServerErrorResponse(Exception exception, string responeMessage)
        {
            bool isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                Response.StatusCode = 500;
                _logger.LogError(exception, responeMessage);
                return Json(new { response = responeMessage });
            }

            throw exception;
        }
    }
}
