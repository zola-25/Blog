using System.Diagnostics;
using System.Threading.Tasks;
using Blog.Web.Extensions;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ErrorController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            return Handler(Response.StatusCode);
        }

        public IActionResult Handler(int code)
        {
            if (code == StatusCodes.Status403Forbidden)
            {
                if (Request.IsAjaxRequest())
                {
                    return Problem("Access to this content is restricted.");
                } 
                return View("~/Views/Error/Forbidden.cshtml");
            }

            if (code == StatusCodes.Status404NotFound)
            {
                if (Request.IsAjaxRequest())
                {
                    return Problem("The page was not found. ");
                }
                return View("~/Views/Error/NotFound.cshtml");
            }
            
            var errorDetails = new ErrorDetails() {
                StatusCode = code,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            if (_webHostEnvironment.IsDevelopment()
                || _configuration.GetValue<bool>("Exceptions:AllowExceptionDetails"))
            {
                var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                errorDetails.ErrorMessage = exceptionHandler?.Error?.ToString();
            }
            else
            {
                errorDetails.ErrorMessage =
                    "Something has gone wrong. ";
            }

            if (Request.IsAjaxRequest())
            {
                return Problem(errorDetails.ErrorMessage);
            }

            return View("~/Views/Error/Error.cshtml", errorDetails);
                
        }
    }
}