using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;
using Blog.Data.Models;
using AutoMapper;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDbContext _blogContext;
        private IMapper _mapper;

        public HomeController(BlogDbContext blogContext, IMapper mapper)
        {
            _mapper = mapper;
            _blogContext = blogContext;
        }

        public IActionResult Index()
        {
            var dbPost = _blogContext
                .Posts
                .OrderByDescending(c => c.CreationDate)
                .First();

            var viewPost = _mapper.Map<Data.Models.Post, ViewModels.Post>(dbPost);

            return View(viewPost);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
