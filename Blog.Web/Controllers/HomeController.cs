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
        private BlogDbContext _bloggingContext;
        private ISnapshotText _snapshotText;
        private IMapper _mapper;

        public HomeController(BlogDbContext blogContext, ISnapshotText snapshotText, IMapper mapper)
        {
            _bloggingContext = blogContext;
            _snapshotText = snapshotText;
            _mapper = mapper;
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
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         
        public IActionResult Post(string permalink)
        {
            var dbPost = _bloggingContext
                .Posts
                .Single(c => c.Permalink == permalink);

            var viewPost = _mapper.Map<Data.Models.Post, ViewModels.Post>(dbPost);

            return View(viewPost);
        }
    }
}
