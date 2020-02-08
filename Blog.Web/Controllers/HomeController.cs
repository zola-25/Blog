using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.Data.Models;
using AutoMapper;
using Blog.Web.ViewModels;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext _blogContext;
        private readonly IMapper _mapper;
        private IHostEnvironment _hostEnvironment;

        public HomeController(BlogDbContext blogContext, IMapper mapper, IHostEnvironment hostEnvironment)
        {
            _blogContext = blogContext;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        // catches all unmatched routes
        [HttpGet("{*url}", Order = int.MaxValue)]
        public IActionResult Index()
        {
            var dbPosts = _blogContext
                .Posts
                .OrderByDescending(c => c.CreationDate)
                .ToList();

            var homeViewModel = new Home
            {
                BlogPost = _mapper.Map<Post, BlogPost>(dbPosts.First()),
                BlogPostLinks = _mapper.Map<List<Post>, List<BlogPostLink>>(dbPosts),
                IsDevelopment = _hostEnvironment.IsDevelopment()
            };
            
            return View(homeViewModel);
        }

        
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         
        public IActionResult Post(string permalink)
        {
            var dbPost = _blogContext
                .Posts
                .Single(c => c.Permalink == permalink);

            var viewPost = _mapper.Map<Data.Models.Post, BlogPost>(dbPost);

            return View(viewPost);
        }
    }
}
