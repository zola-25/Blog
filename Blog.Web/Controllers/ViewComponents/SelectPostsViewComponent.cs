using Blog.Data.Models;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Blog.Controllers.ViewComponents
{
    public class SelectPostsViewComponent : ViewComponent
    {
        public BlogDbContext _blogContext;

        public SelectPostsViewComponent(BlogDbContext blogContext)
        {
            _blogContext = blogContext;
        }

        public IViewComponentResult Invoke(int selectedPostId)
        {
            var posts = _blogContext.Posts
                .Select(c=> new SelectListItem { Text = $"{c.Title}", Value = c.Id.ToString(), Selected = c.Id == selectedPostId })
                .ToList();

            posts.Insert(0, new SelectListItem { Text = $"Create New Post", Value = "0", Selected = selectedPostId == 0 });
            
            return View("~/Views/Shared/Components/SelectPosts.cshtml", new SelectPosts {
                BlogPosts = posts
            });;
        }
    }
}
