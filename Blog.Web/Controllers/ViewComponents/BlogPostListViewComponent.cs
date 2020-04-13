using AutoMapper;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers.ViewComponents
{
    public class BlogPostListViewComponent : ViewComponent
    {
        private readonly BlogDbContext _blogContext;
        private readonly IMapper _mapper;


        public BlogPostListViewComponent(BlogDbContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewPosts = await _blogContext
                .Posts
                .OrderByDescending(c => c.CreationDate)
                .Select(c => _mapper.Map<Web.ViewModels.Post>(c))
                .ToListAsync();

            foreach (var viewPost in viewPosts.Take(5))
            {
                viewPost.LatestFive = true;
            }
            

            return View("~/Views/Shared/Components/BlogPostList.cshtml", viewPosts);
        }
    }
}
