using AutoMapper;
using Blog.Data.Models;
using Blog.ViewModels;
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
                .Take(5)
                .Select(c => _mapper.Map<ViewModels.Post>(c))
                .ToListAsync();

            return View(viewPosts);
        }
    }
}
