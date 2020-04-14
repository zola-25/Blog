using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.Data.Models;
using AutoMapper;
using Blog.Web.ViewModels;
using System.Net;
using System;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDbContext _blogContext;
        private IMapper _mapper;

        public HomeController(BlogDbContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var dbPost = _blogContext
                .Posts
                .OrderByDescending(c => c.CreationDate)
                .First();

            var viewPost = _mapper.Map<Data.Models.Post, Web.ViewModels.Post>(dbPost);

            var uri = GetRequestUri();
            viewPost.FullLink = uri.ToString();
            viewPost.FullLinkEncoded = WebUtility.UrlEncode(viewPost.FullLink);

            return View(viewPost);
        }

        private Uri GetRequestUri()
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Request.Scheme,
                Host = Request.Host.Host,
                Path = Request.Path.ToString(),
                Query = Request.QueryString.ToString()
            };

            return uriBuilder.Uri;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Post/{urlSegment}")]
        public IActionResult Post(string urlSegment)
        {
            var dbPost = _blogContext
                .Posts
                .Single(c => c.UrlSegment == urlSegment);

            var viewPost = _mapper.Map<Data.Models.Post, Web.ViewModels.Post>(dbPost);

            return View("~/Views/Home/Index.cshtml", viewPost);
        }
    }
}
