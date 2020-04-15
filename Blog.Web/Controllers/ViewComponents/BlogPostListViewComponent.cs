using AutoMapper;
using Blog.Data.Models;
using Blog.Web.Services;
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
        private readonly ILinkUtilities _linkUtilities;

        public BlogPostListViewComponent(BlogDbContext blogContext, IMapper mapper, ILinkUtilities linkUtilities)
        {
            _blogContext = blogContext;
            _mapper = mapper;
            _linkUtilities = linkUtilities;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dbPosts = await _blogContext
                .Posts
                .Where(c=>!c.Hidden)
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();

            var viewPosts = dbPosts.Select(c => {
                    var post = _mapper.Map<Web.ViewModels.Post>(c);
                    post.Permalink = _linkUtilities.GetPermalink(c.UrlSegment);
                    post.Path = _linkUtilities.GetPath(c.UrlSegment);
                    return post;
                }).ToList();

            foreach (var viewPost in viewPosts.Take(5))
            {
                viewPost.LatestFive = true;
            }
            
            return View("~/Views/Shared/Components/BlogPostList.cshtml", viewPosts);
        }
    }
}
