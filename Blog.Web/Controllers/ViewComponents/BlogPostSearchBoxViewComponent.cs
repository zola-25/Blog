using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.ViewComponents
{
    public class BlogPostSearchBoxViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View(new SearchRequest());
        }
    }
}
