using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers.ViewComponents
{
    public class BlogPostSearchBoxViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/BlogPostSearchBox.cshtml", new SearchRequest());
        }
    }
}
